using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_api.Models
{
    public class Application
    {
        [Key]
        
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Company { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        public DateTime DateApplied { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty; // Applied/Interview/Offer/Rejected

        public string? Notes { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public ICollection<StatusHistory> StatusHistories { get; set; } = [];
    }
}