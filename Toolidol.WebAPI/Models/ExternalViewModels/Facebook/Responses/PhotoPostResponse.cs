using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses
{
    public class PhotoPostResponse
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("post_id")]
        public string? PostId { get; set; }
    }

    public class PhotoDetailsResponse : BasePostResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("alt_text_custom")]
        public string? AltTextCustom { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [JsonPropertyName("picture")]
        public string? Picture { get; set; }

        [JsonPropertyName("images")]
        public List<Image>? Images { get; set; }

        [JsonPropertyName("place")]
        public Place? Place { get; set; }
    }

    public class PhotoListResponse
    {
        [JsonPropertyName("data")]
        public required List<PhotoDetailsResponse> Data { get; set; } = new();

        [JsonPropertyName("paging")]
        public Paging? Paging { get; set; }
    }
}
