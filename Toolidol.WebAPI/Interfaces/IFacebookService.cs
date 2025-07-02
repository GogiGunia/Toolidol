using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses;

namespace Toolidol.WebAPI.Interfaces
{
    public interface IFacebookService
    {
        /// <summary>
        /// Orchestrates the full authentication flow. It takes a short-lived token,
        /// exchanges it for a long-lived user token, and then retrieves the
        /// long-lived page access tokens for all pages the user manages.
        /// </summary>
        /// <param name="shortLivedUserToken">The token received from the client-side Facebook Login.</param>
        /// <returns>A list of pages with their details and long-lived access tokens.</returns>
        Task<List<PageAccountResponse>> AuthenticateAndGetPageTokensAsync(string shortLivedUserToken);
    }
}
