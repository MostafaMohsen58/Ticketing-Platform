using System.ComponentModel.DataAnnotations;
namespace Tixora.Models.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Available quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Available quantity must be greater than zero.")]
        [Display(Name = "Available Quantity")]
        public int AvailableQuantity { get; set; }

        
        [Required(ErrorMessage = "Event is required.")]
        [Display(Name = "Event")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Ticket category is required.")]
        [Display(Name = "Ticket Category")]
        public int TicketCategoryId { get; set; }

        public TicketStatus Status { get; set; }

        public IEnumerable<Event>? Events { get; set; }
        public IEnumerable<TicketCategory>? TicketCategories { get; set; }
    }
}
