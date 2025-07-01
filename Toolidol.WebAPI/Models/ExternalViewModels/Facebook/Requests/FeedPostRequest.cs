using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Requests
{
    public class FeedPostRequest : BasePostRequest
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [JsonPropertyName("call_to_action")]
        public CallToAction? CallToAction { get; set; }

        [JsonPropertyName("child_attachments")]
        public List<ChildAttachment>? ChildAttachments { get; set; }

        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        [JsonPropertyName("object_attachment")]
        public string? ObjectAttachment { get; set; }

        [JsonPropertyName("multi_share_end_card")]
        public bool? MultiShareEndCard { get; set; }

        [JsonPropertyName("multi_share_optimized")]
        public bool? MultiShareOptimized { get; set; }
    }
}
