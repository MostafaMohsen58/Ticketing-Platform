using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;
using Tixora.ViewModels.BookingViewModel;

namespace Tixora.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly IEventsService eventsService;
        private readonly ITicketService ticketService;
        public BookingController(IBookingService _bookingService,
            IEventsService _eventsService,
            ITicketService _ticketService)
        {
            bookingService = _bookingService;
            eventsService = _eventsService;
            ticketService = _ticketService;
        }
        // Booking
        public async Task<IActionResult> Index()
        {
            var bookings = await bookingService.GetAllAsync();
            return View(bookings);
        }
        // Booking/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            return View(booking);
        }
        // Booking/Create?eventId=1
        [HttpGet]
        public async Task<IActionResult> Create(int eventId)
        {
            var eventDetails =await eventsService.GetById(eventId);
            var availableTickets = await eventsService.GetAvailableTicketsAsync(eventId);
            var viewModel = new CreateBookingViewModel
            {
                EventId = eventId,
                EventTitle = eventDetails.Title,
                EventImageUrl = Url.Content($"~/images/{eventDetails.ImageUrl}"),
                VenueName = eventDetails.Venue.Name,
                EventDate = eventDetails.StartDate,
                AvailableTickets = availableTickets.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.TicketCategory.Name} - {t.Price:C} (Available: {t.AvailableQuantity})"
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                await AvailableTickets(viewModel);
                return View(viewModel);
            }
            try
            {
                var ticket =await ticketService.GetById(viewModel.TicketId);
                if (ticket.AvailableQuantity < viewModel.Amount)
                {
                    ModelState.AddModelError("Amount", $"Not enough tickets available. Only {ticket.AvailableQuantity} left.");
                    await AvailableTickets(viewModel);
                    return View(viewModel);
                }
                viewModel.CalculateTotal(ticket.Price);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pendingBooking = await bookingService.CreatePendingBooking(new BookingRequest
                {
                    EventId = viewModel.EventId,
                    TicketId = viewModel.TicketId,
                    Quantity = viewModel.Amount,
                    UserId = userId,
                    TotalAmount = viewModel.TotalPrice,
                    TicketType = ticket.TicketCategory.Name
                });

                return RedirectToAction("StripeCheckout","Payment", new { bookingId = pendingBooking.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating booking: {ex.Message}");
                await AvailableTickets(viewModel);
                return View(viewModel);
            }
        }
        private async Task AvailableTickets(CreateBookingViewModel viewModel)
        {
            var availableTickets = await eventsService.GetAvailableTicketsAsync(viewModel.EventId);
            viewModel.AvailableTickets = availableTickets.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $"{t.TicketCategory?.Name} - {t.Price:C} (Available: {t.AvailableQuantity})"
            }).ToList();
        }

        // Booking/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            return View(booking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await bookingService.DeleteAsync(id);
                await bookingService.SaveAsync();

                TempData["SuccessMessage"] = "Booking deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting booking: {ex.Message}");
                var booking = await bookingService.GetByIdAsync(id);
                return View(booking);
            }
        }
    }
}