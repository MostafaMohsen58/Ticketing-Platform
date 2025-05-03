using System.ComponentModel.DataAnnotations;

namespace Tixora.ViewModels.BookingViewModel
{
    public class ConfirmationViewModel
    {
        public int BookingId { get; set; }
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string EventImage { get; set; }
        [Display(Name = "Venue Address")]
        public string VenueName { get; set; }
        [Display(Name = "Ticket Type")]
        public string TicketType { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Total Price")]
        public decimal TicketPrice { get; set; }
        public decimal TotalPrice { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }
    }
}
