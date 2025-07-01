using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BackdatedTimeGranularity { Year, Month, Day, Hour, Minute, None }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusType
    {
        [JsonPropertyName("added_photos")] AddedPhotos,
        [JsonPropertyName("added_video")] AddedVideo,
        [JsonPropertyName("app_created_story")] AppCreatedStory,
        [JsonPropertyName("approved_friend")] ApprovedFriend,
        [JsonPropertyName("created_event")] CreatedEvent,
        [JsonPropertyName("created_group")] CreatedGroup,
        [JsonPropertyName("created_note")] CreatedNote,
        [JsonPropertyName("mobile_status_update")] MobileStatusUpdate,
        [JsonPropertyName("published_story")] PublishedStory,
        [JsonPropertyName("shared_story")] SharedStory,
        [JsonPropertyName("tagged_in_photo")] TaggedInPhoto,
        [JsonPropertyName("wall_post")] WallPost
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentCategory
    {
        [JsonPropertyName("BEAUTY_FASHION")] BeautyFashion,
        [JsonPropertyName("BUSINESS")] Business,
        [JsonPropertyName("CARS_TRUCKS")] CarsTrucks,
        [JsonPropertyName("COMEDY")] Comedy,
        [JsonPropertyName("CUTE_ANIMALS")] CuteAnimals,
        [JsonPropertyName("ENTERTAINMENT")] Entertainment,
        [JsonPropertyName("FAMILY")] Family,
        [JsonPropertyName("FOOD_HEALTH")] FoodHealth,
        [JsonPropertyName("HOME")] Home,
        [JsonPropertyName("LIFESTYLE")] Lifestyle,
        [JsonPropertyName("MUSIC")] Music,
        [JsonPropertyName("NEWS")] News,
        [JsonPropertyName("POLITICS")] Politics,
        [JsonPropertyName("SCIENCE")] Science,
        [JsonPropertyName("SPORTS")] Sports,
        [JsonPropertyName("TECHNOLOGY")] Technology,
        [JsonPropertyName("VIDEO_GAMING")] VideoGaming,
        [JsonPropertyName("OTHER")] Other
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UploadPhase { Start, Transfer, Finish, Cancel }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OriginalProjectionType
    {
        [JsonPropertyName("equirectangular")] Equirectangular,
        [JsonPropertyName("cubemap")] Cubemap,
        [JsonPropertyName("half_equirectangular")] HalfEquirectangular
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SwapMode { Replace }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UnpublishedContentType
    {
        [JsonPropertyName("SCHEDULED")] Scheduled,
        [JsonPropertyName("SCHEDULED_RECURRING")] ScheduledRecurring,
        [JsonPropertyName("DRAFT")] Draft,
        [JsonPropertyName("ADS_POST")] AdsPost,
        [JsonPropertyName("INLINE_CREATED")] InlineCreated,
        [JsonPropertyName("PUBLISHED")] Published,
        [JsonPropertyName("REVIEWABLE_BRANDED_CONTENT")] ReviewableBrandedContent
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VideoState { Draft, Published, Scheduled }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReelUploadPhase { START, FINISH }
}
