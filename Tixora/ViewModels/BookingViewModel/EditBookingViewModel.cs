using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tixora.ViewModels.BookingViewModel
{
    public class EditBookingViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        [Display(Name = "Ticket Type")]
        public int TicketId { get; set; }
        public int CurrentQuantity { get; set; }
        public string EventTitle { get; set; }
        public string CurrentTicketType { get; set; }
        public List<SelectListItem> AvailableTickets { get; set; }

    }
}
