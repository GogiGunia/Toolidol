using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses
{
    public class FacebookErrorResponse
    {
        [JsonPropertyName("error")]
        public required FacebookError Error { get; set; }
    }

    public class FacebookError
    {
        [JsonPropertyName("message")]
        public required string Message { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("code")]
        public required int Code { get; set; }

        [JsonPropertyName("error_subcode")]
        public int? ErrorSubcode { get; set; }

        [JsonPropertyName("fbtrace_id")]
        public string? FbtraceId { get; set; }
    }
}
