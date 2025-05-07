using System.ComponentModel.DataAnnotations;

namespace Tixora.ViewModels.BookingViewModel
{
    public class BookingRequest
    {
        [Required]
        public int EventId { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public string TicketType { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10.")]
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
