using backend_api.Common;
using backend_api.Data;
using backend_api.DTO.JobApplication.Create;
using backend_api.Models;
using backend_api.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Service
{

    public class ApplicationService(AppDbContext _context) : IApplicationService
    {
        public async Task<ServiceResult<CreateApplicationResponse>> CreateApplication(CreateApplicationRequest request,int userId)
        {
            if(request.DateApplied > DateTime.UtcNow)
                return ServiceResult<CreateApplicationResponse>.Fail(
                    "DateApplied cannot be more than currrent date",
                    "DATE_ERROR");

            if (await _context.Applications.AnyAsync(a =>
            a.UserId == userId &&
            a.Role == request.Role &&
            a.CompanyName == request.CompanyName))
            {
                return  ServiceResult<CreateApplicationResponse>.Fail(
                    "An application for this company and role already exists.", 
                    "DUPLICATE");
            }

            var result = new Application
            {
                UserId = userId,
                CompanyName = request.CompanyName,
                Role=request.Role,
                Job = request.Job,
                Salary = request.Salary,
                DateApplied = request.DateApplied,
                Status = request.Status,
                JobLink = request.JobLink,
                CreatedAt = DateTime.UtcNow,

            };
            

            await _context.Applications.AddAsync(result);
            await _context.SaveChangesAsync();

            return  ServiceResult<CreateApplicationResponse>.Ok(new CreateApplicationResponse
            {
                Id= result.Id
            });
        }

       
    }
}
