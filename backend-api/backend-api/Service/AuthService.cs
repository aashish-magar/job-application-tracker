using backend_api.Data;
using backend_api.DTO;
using backend_api.Models;
using backend_api.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public AuthService(
            AppDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<string> RegisterUser(AuthDto request)
        {
            var checkEmail = await _context.Users
                .AnyAsync(u => u.Email == request.email);

            if (checkEmail)
                return "User already exists with that email";

            var user = new User
            {
                Email = request.email,
                UserName = request.email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager
                .CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                return string.Join(", ",
                    result.Errors.Select(e => e.Description));
            }

            return "User created successfully";
        }

        public async Task<bool> LoginUser(AuthDto request)
        {
            var checkUser = await _context.Users.
                        FirstOrDefaultAsync(u => u.Email == request.email);

            if (checkUser == null) return false;

            var result = await _userManager
                .CheckPasswordAsync(checkUser, request.password);  
            
            return result;
        }
    }
}