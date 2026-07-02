

using backend_api.Service;
using Microsoft.AspNetCore.Mvc;
using backend_api.Common;
using backend_api.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;

namespace backend_api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService service;

        public AuthController(AuthService _service)
        {
            service = _service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await service.RegisterUser(request);

            if (!result.Success)
            {
                return result.ErrorCode switch
                {
                    "EMAIL_EXISTS" => Conflict(new {error = result.Error}),
                    "CREATION_FAILED" => BadRequest(new {error = result.Error}),
                    _ => StatusCode(500, new { error = "Unexpected error" })
                };
            }
            return Ok(result.Data);
           
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            
            var result = await service.LoginUser(request);
            if (!result.Success)
            {
                return result.ErrorCode switch
                {
                    "EMAIL_NOT_EXISTS" => NotFound(new { error = result.Error }),
                    "FAILED_SIGN_IN" => BadRequest(new { error = result.Error }),
                    _ => StatusCode(500, new { error = "Unexpected error" })
                };
            }
            return Ok(result.Data);
        }
        
    }
}
