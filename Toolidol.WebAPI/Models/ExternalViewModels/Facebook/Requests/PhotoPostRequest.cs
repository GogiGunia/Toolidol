using System.Text.Json.Serialization;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Common;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Requests
{
    public class PhotoPostRequest : BaseMediaRequest
    {
        [JsonPropertyName("caption")]
        public string? Caption { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("privacy")]
        public Privacy? Privacy { get; set; }

        [JsonPropertyName("tags")]
        public List<PhotoTagRequest>? Tags { get; set; }

        [JsonPropertyName("alt_text_custom")]
        public string? AltTextCustom { get; set; }

        [JsonPropertyName("allow_spherical_photo")]
        public bool? AllowSphericalPhoto { get; set; }

        [JsonPropertyName("temporary")]
        public bool? Temporary { get; set; }
    }
}
