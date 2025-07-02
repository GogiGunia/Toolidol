using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses
{
    public class PageAccountResponse
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("tasks")]
        public List<string>? Tasks { get; set; }
    }

    /// <summary>
    /// Represents the top-level object returned by the /me/accounts endpoint,
    /// containing a list of pages and paging information.
    /// </summary>
    public class PageAccountListResponse
    {
        [JsonPropertyName("data")]
        public required List<PageAccountResponse> Data { get; set; }

        // You can add a Paging property here if you need to handle pagination
        // [JsonPropertyName("paging")]
        // public Paging? Paging { get; set; }
    }
}
