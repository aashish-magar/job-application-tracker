using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_api.Models
{
    public class StatusHistory
    {

 
        
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; } = null!;
        public ApplicationStatus Status { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        
    }
}