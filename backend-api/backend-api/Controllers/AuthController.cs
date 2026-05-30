using backend_api.DTO;
using backend_api.Service;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register(AuthDto request)
        {
            var result = await service.RegisterUser(request);
            if (result == "User Created Successfully")
            {
                return Ok(result);

            }
            else
            {
                return BadRequest(result);

            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthDto request)
        {
            if(await service.LoginUser(request))
                return Ok("User logged in successfully");
            else
                return BadRequest("Invalid email or password");
        }
    }
}
