// DTOs/Auth/RegisterRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace backend_api.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}