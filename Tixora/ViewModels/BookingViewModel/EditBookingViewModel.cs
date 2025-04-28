using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class EditBookingViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Event ID is required")]
    public int EventId { get; set; }

    public string EventTitle { get; set; }

    [Required(ErrorMessage = "Current quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int CurrentQuantity { get; set; }

    [Required(ErrorMessage = "New quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int NewQuantity { get; set; }

    [Required(ErrorMessage = "Ticket selection is required")]
    public int TicketId { get; set; }

    public string CurrentTicketType { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> AvailableTickets { get; set; } = new List<SelectListItem>();
}