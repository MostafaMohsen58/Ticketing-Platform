namespace Tixora.ViewModels.BookingViewModel
{
    public class ConfirmationViewModel
    {
        public int BookingId { get; set; }

        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string VenueName { get; set; }
        public string TicketType { get; set; }
        public int Quantity { get; set; }
        //public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
