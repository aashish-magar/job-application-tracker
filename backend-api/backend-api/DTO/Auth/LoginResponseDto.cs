// DTOs/Auth/LoginResponseDto.cs
namespace backend_api.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;        // for JWT later
        //public DateTime TokenExpiresAt { get; set; }             // for JWT later
        //public IList<string> Roles { get; set; } = new List<string>();
    }
}