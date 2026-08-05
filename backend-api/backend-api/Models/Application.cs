using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks.Dataflow;

namespace backend_api.Models
{
    public class Application
    {
        
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string CompanyName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public JobType Job { get; set; }
        public string? Salary { get; set; }

        public DateTime DateApplied { get; set; }
        public ApplicationStatus Status { get; set; } // Applied/Interview/Offer/Rejected
        public string? JobLink { get; set; }

        public string? Notes { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<StatusHistory> StatusHistories { get; set; } = [];
    }
    public enum ApplicationStatus
    {
        Applied =1,
        Interview=2,
        Offer=3,
        Rejected=4
    }
    public enum JobType
    {
        FullTime = 1,
        Internship =2,
        Contract =3
    }
    
}