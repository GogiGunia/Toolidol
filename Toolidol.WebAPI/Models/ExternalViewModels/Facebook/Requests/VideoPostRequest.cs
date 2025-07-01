using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Requests
{
    public class VideoPostRequest : BaseMediaRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("file_url")]
        public string? FileUrl { get; set; }

        [JsonPropertyName("thumb")]
        public string? Thumb { get; set; }

        [JsonPropertyName("content_category")]
        public ContentCategory? ContentCategory { get; set; }

        [JsonPropertyName("embeddable")]
        public bool? Embeddable { get; set; }

        // Resumable Upload Fields
        [JsonPropertyName("upload_phase")]
        public UploadPhase? UploadPhase { get; set; }

        [JsonPropertyName("upload_session_id")]
        public string? UploadSessionId { get; set; }

        [JsonPropertyName("start_offset")]
        public long? StartOffset { get; set; }

        [JsonPropertyName("file_size")]
        public long? FileSize { get; set; }
    }
}
