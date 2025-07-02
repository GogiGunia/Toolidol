using Toolidol.Model.Model;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses;

namespace Toolidol.WebAPI.Interfaces
{
    public interface IFacebookPageService
    {
        /// <summary>
        /// Saves or updates page tokens for a user.
        /// </summary>
        Task SaveOrUpdatePageTokensAsync(int userId, List<PageAccountResponse> pageResponses);

        /// <summary>
        /// Gets all Facebook pages connected to a user.
        /// </summary>
        Task<List<FacebookPage>> GetUserPagesAsync(int userId);

        /// <summary>
        /// Disconnects all Facebook pages for a user.
        /// </summary>
        Task DisconnectUserPagesAsync(int userId);

        /// <summary>
        /// Gets a decrypted access token for a specific page (backend use only).
        /// </summary>
        Task<string?> GetDecryptedPageTokenAsync(int userId, string pageId);

        /// <summary>
        /// Checks if a user has any connected Facebook pages.
        /// </summary>
        Task<bool> HasConnectedPagesAsync(int userId);
    }
}
