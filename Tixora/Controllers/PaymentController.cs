using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Tixora.Services;
using Tixora.Services.Interfaces;
using Tixora.ViewModels.BookingViewModel;

namespace Tixora.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IBookingService _bookingService;
        public PaymentController(IPaymentService paymentService, IBookingService bookingService)
        {
            _paymentService = paymentService;
            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> InitiateBooking(BookingRequest request)
        {
            if (!ModelState.IsValid) return View("", request);

            var pendingBooking = await _bookingService.CreatePendingBooking(request);

            return RedirectToAction("StripeCheckout", new { bookingId = pendingBooking.Id });
        }
        [HttpGet]
        public async Task<IActionResult> StripeCheckout(int bookingId)
        {
            var sessionUrl = await _paymentService.CreateStripeSession(bookingId);
            return Redirect(sessionUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Success(string session_Id)
        {
            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(session_Id);
            if (session.PaymentStatus != "paid") return RedirectToAction("Failed");

            var booking = await _bookingService.GetByStripeSessionIdAsync(session_Id);
            if (booking == null) return RedirectToAction("Failed");

            if (booking.PaymentStatus == "Paid")
            {
                ViewBag.ContactInfo = "For any questions, please contact support@gmail.com";
                return View();
            }

            var isConfirmed = await _bookingService.UpdateAfterPay(booking);
            if (!isConfirmed) return RedirectToAction("Failed");
            ConfirmationViewModel confirmationViewModel = new ConfirmationViewModel
            {
                BookingId = booking.Id,
                EventTitle = booking.Ticket.Event.Title,
                EventImage = booking.Ticket.Event.ImageUrl,
                TicketType = booking.Ticket.TicketCategory.Name,
                TicketPrice = booking.Ticket.Price,
                TotalPrice = booking.TotalAmount,
                PaymentDate = DateTime.UtcNow,
                Quantity = booking.TicketQuantity,
                EventDate = booking.Ticket.Event.StartDate,
                VenueName = booking.Ticket.Event.Venue.Name,
            };

            ViewBag.ContactInfo = "For any questions, please contact support@gmail.com";

            return View(confirmationViewModel);
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            ViewBag.Message = "Payment was canceled.";
            return View();
        }
    }
}
