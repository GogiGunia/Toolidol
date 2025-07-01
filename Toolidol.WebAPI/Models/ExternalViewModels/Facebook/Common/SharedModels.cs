using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common
{
    // Fully defined shared classes
    public class CallToAction
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("value")]
        public required CallToActionValue Value { get; set; }
    }

    public class CallToActionValue
    {
        [JsonPropertyName("link")]
        public required string Link { get; set; }
    }

    public class ChildAttachment
    {
        [JsonPropertyName("link")]
        public required string Link { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class Targeting
    {
        [JsonPropertyName("geo_locations")]
        public GeoLocations? GeoLocations { get; set; }

        [JsonPropertyName("locales")]
        public List<int>? Locales { get; set; }
    }

    public class GeoLocations
    {
        [JsonPropertyName("countries")]
        public List<string>? Countries { get; set; }

        [JsonPropertyName("cities")]
        public List<string>? Cities { get; set; }

        [JsonPropertyName("regions")]
        public List<int>? Regions { get; set; }
    }

    public class FeedTargeting : Targeting { }

    public class Privacy
    {
        [JsonPropertyName("value")]
        public string? Value { get; set; } // EVERYONE, ALL_FRIENDS, SELF, CUSTOM

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("allow")]
        public string? Allow { get; set; } // Comma-separated IDs

        [JsonPropertyName("deny")]
        public string? Deny { get; set; } // Comma-separated IDs
    }

    public class Paging
    {
        [JsonPropertyName("cursors")]
        public required Cursors Cursors { get; set; }

        [JsonPropertyName("next")]
        public string? Next { get; set; }
    }

    public class Cursors
    {
        [JsonPropertyName("before")]
        public required string Before { get; set; }

        [JsonPropertyName("after")]
        public required string After { get; set; }
    }

    public class From
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }

    public class ActionSummary
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("link")]
        public required string Link { get; set; }
    }

    public class Shares
    {
        [JsonPropertyName("count")]
        public required int Count { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("offset")]
        public int? Offset { get; set; }

        [JsonPropertyName("length")]
        public int? Length { get; set; }
    }

    public class PhotoTagRequest
    {
        [JsonPropertyName("tag_uid")]
        public string? TagUid { get; set; }

        [JsonPropertyName("tag_text")]
        public string? TagText { get; set; }

        [JsonPropertyName("x")]
        public float X { get; set; }

        [JsonPropertyName("y")]
        public float Y { get; set; }
    }

    public class Attachment
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("description_tags")]
        public List<Tag>? DescriptionTags { get; set; }

        [JsonPropertyName("media")]
        public Media? Media { get; set; }

        [JsonPropertyName("target")]
        public Target? Target { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class AttachmentsNode
    {
        [JsonPropertyName("data")]
        public List<Attachment>? Data { get; set; }
    }

    public class TagsNode
    {
        [JsonPropertyName("data")]
        public List<Tag>? Data { get; set; }
    }


    public class Media
    {
        [JsonPropertyName("image")]
        public Image? Image { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("height")]
        public required int Height { get; set; }

        [JsonPropertyName("src")]
        public required string Src { get; set; }

        [JsonPropertyName("width")]
        public required int Width { get; set; }
    }

    public class Target
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }
    }

    public class Place
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("location")]
        public Location? Location { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }
    }
}
