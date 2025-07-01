using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Requests
{
    public class VideoReelPostRequest
    {
        [JsonPropertyName("upload_phase")]
        public required ReelUploadPhase UploadPhase { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("video_id")]
        public string? VideoId { get; set; }

        [JsonPropertyName("video_state")]
        public VideoState? VideoState { get; set; }

        [JsonPropertyName("scheduled_publish_time")]
        public long? ScheduledPublishTime { get; set; }
    }
}
