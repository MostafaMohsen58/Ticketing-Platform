using Microsoft.AspNetCore.Mvc;
using Stripe;
using Tixora.Services;
using Tixora.Services.Interfaces;
using Tixora.ViewModels.BookingViewModel;

namespace Tixora.Controllers
{
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
    }
}
