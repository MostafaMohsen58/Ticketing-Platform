using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
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
        private readonly ITicketRepository ticketRepository;
        public BookingController(IBookingService _bookingService,
            IEventsService _eventsService,
            ITicketRepository _ticketRepository)
        {
            bookingService = _bookingService;
            eventsService = _eventsService;
            ticketRepository = _ticketRepository;
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
            var eventDetails = eventsService.GetById(eventId);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await bookingService.CreateBookingAsync(viewModel,userId);
                return RedirectToAction("Confirmation", new { id = booking.Id });
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
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new ConfirmationViewModel
            {
                BookingId = booking.Id,
                EventTitle = booking.Ticket.Event.Title,
                EventDate = booking.Ticket.Event.StartDate,
                VenueName = booking.Ticket.Event.Venue.Name,
                TicketType = booking.Ticket.TicketCategory.Name,
                Quantity = booking.Amount,
                BookingDate = booking.BookedAt
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            return View(booking);
        }
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (ModelState.IsValid)
            {
                bookingService.UpdateAsync(booking);
                bookingService.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            return View(booking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bookingService.DeleteAsync(id);
            bookingService.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
