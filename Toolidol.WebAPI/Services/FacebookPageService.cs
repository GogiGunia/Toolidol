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

        /// <summary>
        /// Gets all Facebook pages connected to a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>List of connected Facebook pages (without decrypted tokens).</returns>
        public async Task<List<FacebookPage>> GetUserPagesAsync(int userId)
        {
            return await _dbContext.FacebookPages
                .Where(p => p.UserId == userId)
                .Select(p => new FacebookPage
                {
                    Id = p.Id,
                    FacebookPageId = p.FacebookPageId,
                    Name = p.Name,
                    UserId = p.UserId,
                    EncryptedAccessToken = "" // Don't return encrypted token for security
                })
                .ToListAsync();
        }

        /// <summary>
        /// Disconnects all Facebook pages for a user by removing them from the database.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>Task representing the async operation.</returns>
        public async Task DisconnectUserPagesAsync(int userId)
        {
            var userPages = await _dbContext.FacebookPages
                .Where(p => p.UserId == userId)
                .ToListAsync();

            if (userPages.Any())
            {
                _logger.LogInformation("Removing {PageCount} Facebook pages for user {UserId}", userPages.Count, userId);
                _dbContext.FacebookPages.RemoveRange(userPages);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation("No Facebook pages found to disconnect for user {UserId}", userId);
            }
        }

        /// <summary>
        /// Gets a decrypted access token for a specific page (for backend use only).
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="pageId">The Facebook page ID.</param>
        /// <returns>Decrypted access token or null if not found.</returns>
        public async Task<string?> GetDecryptedPageTokenAsync(int userId, string pageId)
        {
            var page = await _dbContext.FacebookPages
                .FirstOrDefaultAsync(p => p.UserId == userId && p.FacebookPageId == pageId);

            if (page == null)
            {
                _logger.LogWarning("Page {PageId} not found for user {UserId}", pageId, userId);
                return null;
            }

            try
            {
                return _dataProtectionService.Decrypt(page.EncryptedAccessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to decrypt access token for page {PageId} and user {UserId}", pageId, userId);
                return null;
            }
        }

        /// <summary>
        /// Checks if a user has any connected Facebook pages.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>True if user has connected pages, false otherwise.</returns>
        public async Task<bool> HasConnectedPagesAsync(int userId)
        {
            return await _dbContext.FacebookPages
                .AnyAsync(p => p.UserId == userId);
        }
    }

}