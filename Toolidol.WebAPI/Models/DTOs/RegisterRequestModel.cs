using System.ComponentModel.DataAnnotations;

namespace Toolidol.WebAPI.Models.DTOs
{
    public class RegisterRequestModel
    {
        [Required]
        public required UserCreateModel UserModel { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public required string Password { get; set; }
    }
}
