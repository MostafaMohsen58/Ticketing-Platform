namespace Tixora.ViewModels.PaymentViewModel
{
    public class StripeCheckoutViewModel
    {
        public int BookingId { get; set; }
        public string EventTitle { get; set; }
        public float TotalAmount { get; set; }
        public string StripePublishableKey { get; set; }
    }
}
