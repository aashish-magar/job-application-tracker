// Service/Interface/IAuthService.cs
using backend_api.Common;
using backend_api.DTOs.Auth;

namespace backend_api.Service.Interface
{
    public interface IAuthService
    {
        Task<ServiceResult<RegisterResponseDto>> RegisterUser(RegisterRequestDto request);
        Task<ServiceResult<LoginResponseDto>> LoginUser(LoginRequestDto request);
    }
}