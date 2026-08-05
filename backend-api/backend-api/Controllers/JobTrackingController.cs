using backend_api.DTO.JobApplication;
using backend_api.DTO.JobApplication.Create;
using backend_api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTrackingController(ApplicationService _appService) : ControllerBase
    {
        [Authorize]
        [HttpPost("CreateJobApplication")]
       public async Task<IActionResult> CreateJobApplication(CreateApplicationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _appService.CreateApplication(request,userId);
            if (!result.Success)
            {
                return result.ErrorCode switch
                {
                    "DATE_ERROR" => BadRequest(new {error = result.Error}),
                    "DUPLICATE" => BadRequest(new {error = result}),
                    _ => StatusCode(500,"unknown error")
                };

            }
            return Ok(result);
        }
    }
}
