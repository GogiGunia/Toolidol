using Toolidol.Model.Model;
using Toolidol.Model;
using Toolidol.WebAPI.Interfaces;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses;
using Microsoft.EntityFrameworkCore;

namespace Toolidol.WebAPI.Services
{
    public class FacebookPageService : IFacebookPageService
    {
        private readonly ToolidolDbContext _dbContext;
        private readonly IDataProtectionService _dataProtectionService;
        private readonly ILogger<FacebookPageService> _logger;

        public FacebookPageService(
            ToolidolDbContext dbContext,
            IDataProtectionService dataProtectionService,
            ILogger<FacebookPageService> logger)
        {
            _dbContext = dbContext;
            _dataProtectionService = dataProtectionService;
            _logger = logger;
        }

        public async Task SaveOrUpdatePageTokensAsync(int userId, List<PageAccountResponse> pageResponses)
        {
            var existingPages = await _dbContext.FacebookPages
                .Where(p => p.UserId == userId).ToListAsync();

            foreach (var pageResponse in pageResponses)
            {
                var existingPage = existingPages.FirstOrDefault(p => p.FacebookPageId == pageResponse.Id);

                var encryptedToken = _dataProtectionService.Encrypt(pageResponse.AccessToken);

                if (existingPage != null)
                {
                    _logger.LogInformation("Updating token for existing page {PageId} for user {UserId}", pageResponse.Id, userId);
                    existingPage.EncryptedAccessToken = encryptedToken;
                    existingPage.Name = pageResponse.Name; 
                }
                else
                {
                    _logger.LogInformation("Adding new page {PageId} for user {UserId}", pageResponse.Id, userId);
                    var newPage = new FacebookPage
                    {
                        UserId = userId,
                        FacebookPageId = pageResponse.Id,
                        Name = pageResponse.Name,
                        EncryptedAccessToken = encryptedToken
                    };
                    await _dbContext.FacebookPages.AddAsync(newPage);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
