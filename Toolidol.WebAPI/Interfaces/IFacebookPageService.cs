using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses;

namespace Toolidol.WebAPI.Interfaces
{
    public interface IFacebookPageService
    {
        /// <summary>
        /// Saves or updates a list of Facebook pages for a specific user.
        /// Access tokens are encrypted before being stored.
        /// </summary>
        Task SaveOrUpdatePageTokensAsync(int userId, List<PageAccountResponse> pageResponses);
    }
}
