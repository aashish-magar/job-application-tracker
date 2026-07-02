// DTOs/Auth/RegisterResponseDto.cs
namespace backend_api.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
    }
}