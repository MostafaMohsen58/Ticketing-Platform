using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tixora.ViewModels
{
    public class CreateBookingViewModel
    {
        // Event Info
        public int EventId { get; set; }

        [Display(Name = "Event Name")]
        public string EventTitle { get; set; }

        [Display(Name = "Event Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EventDate { get; set; }
        public string EventImageUrl { get; set; }
        [Display(Name = "Venue")]
        public string VenueName { get; set; }

        // Ticket Selection
        [Required(ErrorMessage = "Please select a ticket type")]
        [Display(Name = "Ticket Type")]
        public int TicketId { get; set; }
        public List<SelectListItem> AvailableTickets { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "You can book 1-10 tickets only")]
        [Display(Name = "Number of Tickets")]
        public int Amount { get; set; }

        [Display(Name = "Total Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float TotalPrice { get; private set; }

        //// Seats
        //[Display(Name = "Select Seats")]
        //public List<string> SelectedSeats { get; set; } = new List<string>();

        //public List<SelectListItem> AvailableSeats { get; set; } = new List<SelectListItem>();

        public void CalculateTotalPrice(float ticketPrice)
        {
            if (AvailableTickets.Any() && TicketId > 0)
            {
                var selectedTicket = AvailableTickets.FirstOrDefault(t => t.Value == TicketId.ToString());
                if (selectedTicket != null && float.TryParse(selectedTicket.Text, out var price))
                {
                    TotalPrice = price * Amount;
                }
            }
        }
    }
}
