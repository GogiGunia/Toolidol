using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses
{
    public class VideoReelPostResponse
    {
        [JsonPropertyName("video_id")]
        public string? VideoId { get; set; }

        [JsonPropertyName("upload_url")]
        public string? UploadUrl { get; set; }

        [JsonPropertyName("success")]
        public required bool Success { get; set; }

        [JsonPropertyName("post_id")]
        public string? PostId { get; set; }
    }
}
