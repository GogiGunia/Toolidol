using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses
{
    // Response for a resumable upload chunk
    public class VideoUploadResponse
    {
        [JsonPropertyName("upload_session_id")]
        public string? UploadSessionId { get; set; }

        [JsonPropertyName("video_id")]
        public required string VideoId { get; set; }

        [JsonPropertyName("start_offset")]
        public long StartOffset { get; set; }

        [JsonPropertyName("end_offset")]
        public long EndOffset { get; set; }
    }

    // Final success response after publishing
    public class VideoPostResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("post_id")]
        public string? PostId { get; set; }

        [JsonPropertyName("success")]
        public required bool Success { get; set; }
    }

    public class VideoDetailsResponse : BasePostResponse
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("embed_html")]
        public string? EmbedHtml { get; set; }

        [JsonPropertyName("length")]
        public double? Length { get; set; }

        [JsonPropertyName("picture")]
        public string? Picture { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }
    }

    public class VideoListResponse
    {
        [JsonPropertyName("data")]
        public required List<VideoDetailsResponse> Data { get; set; } = new();

        [JsonPropertyName("paging")]
        public Paging? Paging { get; set; }
    }
}
