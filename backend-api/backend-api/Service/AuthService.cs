using backend_api.Common;
using backend_api.Data;
using backend_api.DTOs.Auth;
using backend_api.Models;
using backend_api.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_api.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            AppDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ServiceResult<RegisterResponseDto>> RegisterUser(RegisterRequestDto request)  
        {
            var emailTaken = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (emailTaken)
                return ServiceResult<RegisterResponseDto>.Fail(
                    "Email already in use", "EMAIL_EXISTS");

            var newUser = new User
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
                return ServiceResult<RegisterResponseDto>.Fail(
                    string.Join(", ", result.Errors.Select(e => e.Description)),
                    "CREATION_FAILED");

            return ServiceResult<RegisterResponseDto>.Ok(new RegisterResponseDto
            {
                UserId = newUser.Id,
                Email = newUser.Email!,
                CreatedAt = newUser.CreatedAt,
                
            });
        }

        public async Task<ServiceResult<LoginResponseDto>> LoginUser(LoginRequestDto request)
        {
            var checkUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (checkUser == null)
            {
                return ServiceResult<LoginResponseDto>.Fail(
                    "User with that email does not exist",
                    "EMAIL_NOT_EXISTS");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(
      checkUser,
      request.Password);

            if (!passwordValid)
            {
                return ServiceResult<LoginResponseDto>.Fail(
                    "Invalid email or password",
                    "FAILED_SIGN_IN");
            }

            // Generate token AFTER successful authentication
            var token = GenerateJwtToken(checkUser);

            return ServiceResult<LoginResponseDto>.Ok(new LoginResponseDto
            {
                UserId = checkUser.Id,
                Email = checkUser.Email,
                Token = token
            });
        }
        public  string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email.ToString())
                
            };

            var securityKey = new SymmetricSecurityKey(Encoding
                                    .UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var signinCred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signinCred);

            var tokenDescriptor = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenDescriptor;

    }

        
    }
}