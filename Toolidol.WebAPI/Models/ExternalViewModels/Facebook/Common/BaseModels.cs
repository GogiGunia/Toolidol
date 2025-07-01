using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common
{
    public abstract class BasePostRequest
    {
        [JsonPropertyName("published")]
        public bool Published { get; set; } = true;

        [JsonPropertyName("scheduled_publish_time")]
        public long? ScheduledPublishTime { get; set; }

        [JsonPropertyName("backdated_time")]
        public long? BackdatedTime { get; set; }

        [JsonPropertyName("backdated_time_granularity")]
        public BackdatedTimeGranularity? BackdatedTimeGranularity { get; set; }

        [JsonPropertyName("targeting")]
        public Targeting? Targeting { get; set; }

        [JsonPropertyName("feed_targeting")]
        public FeedTargeting? FeedTargeting { get; set; }

        [JsonPropertyName("place")]
        public string? Place { get; set; }

        [JsonPropertyName("no_story")]
        public bool? NoStory { get; set; }
    }

    public abstract class BaseMediaRequest : BasePostRequest
    {
        [JsonPropertyName("sponsor_id")]
        public string? SponsorId { get; set; }

        [JsonPropertyName("sponsor_relationship")]
        public long? SponsorRelationship { get; set; }

        [JsonPropertyName("unpublished_content_type")]
        public UnpublishedContentType? UnpublishedContentType { get; set; }
    }

    public abstract class BasePostResponse
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("updated_time")]
        public DateTime? UpdatedTime { get; set; }

        [JsonPropertyName("from")]
        public From? From { get; set; }

        [JsonPropertyName("privacy")]
        public Privacy? Privacy { get; set; }
    }
}
