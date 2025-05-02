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
        public decimal TotalPrice { get; set; }

        public void CalculateTotal(decimal ticketPrice)
        {
            TotalPrice = ticketPrice * Amount;
        }
        //// Seats
        //[Display(Name = "Select Seats")]
        //public List<string> SelectedSeats { get; set; } = new List<string>();

        //public List<SelectListItem> AvailableSeats { get; set; } = new List<SelectListItem>();
    }
}
