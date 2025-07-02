using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toolidol.WebAPI.Interfaces;
using Toolidol.WebAPI.Models;
using Toolidol.WebAPI.Security;
using Toolidol.WebAPI.Services;

namespace Toolidol.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FacebookController : ControllerBase
    {
        private readonly IFacebookService _facebookService;
        private readonly IFacebookPageService _facebookPageService;
        private readonly IUserService _userService;
        private readonly ILogger<FacebookController> _logger;

        public FacebookController(
        IFacebookService facebookService,
        IFacebookPageService facebookPageService,
        IUserService userService,
        ILogger<FacebookController> logger)
        {
            _facebookService = facebookService;
            _facebookPageService = facebookPageService;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Receives a short-lived user token from the client, triggers the server-side
        /// flow to get long-lived page tokens, and returns the page information.
        /// </summary>
        /// <param name="request">The request body containing the short-lived token.</param>
        /// <returns>A list of pages the user manages, with their access tokens.</returns>
        [HttpPost("authenticate")]
        [Authorize(Policy = Policy.GENERAL_ACCESS)]
        public async Task<IActionResult> Authenticate([FromBody] FacebookAuthRequest request)
        {
            if (string.IsNullOrEmpty(request.ShortLivedToken))
                throw new ArgumentNullException("Short-lived token is required.");
            try
            {
                var currentUser = await _userService.GetUserAsync(HttpContext);

                var pageAccounts = await _facebookService.AuthenticateAndGetPageTokensAsync(request.ShortLivedToken);

                await _facebookPageService.SaveOrUpdatePageTokensAsync(currentUser.Id, pageAccounts);

                _logger.LogInformation("Successfully authenticated with Facebook and stored page tokens for user {UserId}", currentUser.Id);

                var responseData = pageAccounts.Select(p => new { p.Id, p.Name }); // Return only non-sensitive data
                return Ok(new { message = "Facebook pages connected successfully.", pages = responseData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the Facebook authentication process.");
                return StatusCode(500, new { message = "An internal error occurred while connecting to Facebook." });
            }
        }
    }
}
