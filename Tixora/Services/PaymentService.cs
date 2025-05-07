using Stripe;
using Stripe.Checkout;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;

namespace Tixora.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingService _bookingService;
        private readonly ITicketService _ticketService;

        public PaymentService(IConfiguration config,
                              IPaymentRepository paymentRepository,
                              IBookingService bookingService,
                              ITicketService ticketService)
        {
            _config = config;
            _paymentRepository = paymentRepository;
            _bookingService = bookingService;
            _ticketService = ticketService;

            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }
        public async Task<string> CreateStripeSession(int bookingId)
        {
            var booking = await _bookingService.GetByIdAsync(bookingId);
            var ticketPrice = await _ticketService.GetTicketPriceAsync(booking.TicketId);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(ticketPrice * 100), // Amount in cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Ticket for {booking.Ticket.Event.Title}",
                                Description = $"Ticket Type: {booking.Ticket.TicketCategory.Name} | " + 
                                     $"Unit Price: {booking.TotalAmount/booking.TicketQuantity:C}",
                            },
                        },
                        Quantity = booking.TicketQuantity,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:55308/Payment/Success?session_Id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:55308/Payment/Cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "BookingId", booking.Id.ToString() },
                    { "UserId", booking.UserId },
                },
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            booking.StripeSessionId = session.Id;
            await _bookingService.UpdateBeforePay(booking);

            return session.Url;
        }

    }
}