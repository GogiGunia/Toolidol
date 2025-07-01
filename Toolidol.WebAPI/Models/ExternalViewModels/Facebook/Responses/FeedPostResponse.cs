using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses
{
    public class FeedPostResponse : BasePostResponse
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("message_tags")]
        public TagsNode? MessageTags { get; set; }

        [JsonPropertyName("story")]
        public string? Story { get; set; }

        [JsonPropertyName("story_tags")]
        public TagsNode? StoryTags { get; set; }

        [JsonPropertyName("permalink_url")]
        public string? PermalinkUrl { get; set; }

        [JsonPropertyName("status_type")]
        public StatusType? StatusType { get; set; }

        [JsonPropertyName("is_published")]
        public bool? IsPublished { get; set; }

        [JsonPropertyName("shares")]
        public Shares? Shares { get; set; }

        [JsonPropertyName("attachments")]
        public AttachmentsNode? Attachments { get; set; }
    }

    public class FeedListResponse
    {
        [JsonPropertyName("data")]
        public required List<FeedPostResponse> Data { get; set; } = new();

        [JsonPropertyName("paging")]
        public Paging? Paging { get; set; }
    }
}
