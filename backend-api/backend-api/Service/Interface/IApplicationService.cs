using backend_api.Common;
using backend_api.DTO.JobApplication.Create;

namespace backend_api.Service.Interface
{
    public interface IApplicationService
    {
        Task<ServiceResult<CreateApplicationResponse>> CreateApplication(CreateApplicationRequest request,int userId);
    }
}
