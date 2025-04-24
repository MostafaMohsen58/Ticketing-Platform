using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;

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
        // Booking/Create
        [HttpGet]
        public async Task<IActionResult> Create(int eventId)
        {
            var eventDetails = eventsService.GetById(eventId);
            var tickets = ticketRepository.GetAll();
            var viewModel = new CreateBookingViewModel
            {
                EventId = eventId,
                EventTitle = eventDetails.Title,
                EventImageUrl = eventDetails.ImageUrl,
                VenueName = eventDetails.Venue.Name,
                EventDate = eventDetails.StartDate,
                AvailableTickets = tickets.Select(t => new SelectListItem
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
            if (ModelState.IsValid)
            {
                //viewModel.AvailableTickets = await eventsService.GetAvailableTicketsAsync(viewModel.EventId);
                return View(viewModel);
            }
            try
            {
                var booking = await bookingService.CreateBookingAsync(viewModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating booking: {ex.Message}");
            }
            //viewModel.AvailableTickets = await _eventService.GetAvailableTicketsAsync(viewModel.EventId);
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
