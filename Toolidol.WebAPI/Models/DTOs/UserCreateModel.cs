using Toolidol.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Toolidol.WebAPI.Models.DTOs
{
    public class UserCreateModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }
}
