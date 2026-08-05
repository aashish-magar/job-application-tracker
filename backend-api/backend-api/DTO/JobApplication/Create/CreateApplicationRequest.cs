using backend_api.Models;

namespace backend_api.DTO.JobApplication.Create
{
    public class CreateApplicationRequest
    {

        public string CompanyName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public JobType Job { get; set; }
        public string? Salary { get; set; }

        public DateTime DateApplied { get; set; }
        public ApplicationStatus Status { get; set; } // Applied/Interview/Offer/Rejected
        public string? JobLink { get; set; }

        
    }
}
