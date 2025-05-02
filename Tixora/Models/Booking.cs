using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tixora.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10.")]
        public int TicketQuantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string? StripeSessionId { get; set; }
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Failed

        public DateTime BookedAt { get; set; } = DateTime.UtcNow; // Set a default value for booking time
        public DateTime? PaymentDate { get; set; }

        [Required(ErrorMessage = "Ticket ID is required.")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}