namespace backend_api.DTO
{
    public class AuthDto
    {
     
        public string email { get; set; } = string.Empty;
        public string password { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
