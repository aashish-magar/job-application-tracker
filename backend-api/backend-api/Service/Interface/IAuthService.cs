using backend_api.DTO;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace backend_api.Service.Interface
{
    public interface IAuthService
    {
         Task<String> RegisterUser(AuthDto request);
        Task<bool> LoginUser(AuthDto request);
    }
}
