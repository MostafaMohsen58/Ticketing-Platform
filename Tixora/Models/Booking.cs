using System.ComponentModel.DataAnnotations;

namespace Tixora.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be at least 1.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Transaction ID is required.")]
        public int TransactionId { get; set; }

        public DateTime BookedAt { get; set; } = DateTime.UtcNow; // Set a default value for booking time

        [Required(ErrorMessage = "Ticket ID is required.")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}