using System.ComponentModel.DataAnnotations;
namespace Tixora.Models.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(10.00, double.MaxValue, ErrorMessage = "Price must be greater than 10.00")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Available Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Available Quantity cannot be negative.")]
        public int AvailableQuantity { get; set; }

        [Required(ErrorMessage = "Event ID is required.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Ticket Category ID is required.")]
        public int TicketCategoryId { get; set; }

        public TicketStatus Status { get; set; }

        public IEnumerable<Event>? Events { get; set; }
        public IEnumerable<TicketCategory>? TicketCategories { get; set; }
    }
}
