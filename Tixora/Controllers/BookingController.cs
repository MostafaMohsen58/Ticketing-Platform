using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        // Booking/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            var ticketsPrice = (decimal)(booking.Amount * booking.Ticket.Price);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new ConfirmationViewModel
            {
                BookingId = booking.Id,
                EventTitle = booking.Ticket.Event.Title,
                EventDate = booking.Ticket.Event.StartDate,
                VenueName = booking.Ticket.Event.Venue.Name,
                TicketType = booking.Ticket.TicketCategory.Name,
                Quantity = booking.Amount,
                BookingDate = booking.BookedAt,
                TotalPrice = ticketsPrice
            };
            return View(viewModel);
        }
        public async Task<IActionResult> PrintTicket(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
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
            return View("PrintTicket", viewModel);
        }
        // Booking/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await bookingService.GetByIdAsync(id);
            var viewModel = new EditBookingViewModel
            {
                Id = booking.Id,
                EventId = booking.Ticket.EventId,
                EventTitle = booking.Ticket.Event.Title,
                CurrentQuantity = booking.Amount,
                TicketId = booking.TicketId,
                CurrentTicketType = booking.Ticket.TicketCategory.Name,
                AvailableTickets = await GetAvailableTicketsForEdit(booking.Ticket.EventId, booking.TicketId)
            };
            if (!viewModel.AvailableTickets.Any())
            {
                ViewBag.ErrorMessage = "No available tickets for this event.";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookingViewModel viewModel)
        {
            var booking = await bookingService.GetByIdAsync(id);
            viewModel.EventTitle = booking.Ticket.Event.Title;
            viewModel.EventId = booking.Ticket.EventId;
            viewModel.CurrentTicketType = booking.Ticket.TicketCategory.Name;
            viewModel.CurrentQuantity = booking.Amount;
            viewModel.AvailableTickets = await GetAvailableTicketsForEdit(viewModel.EventId, viewModel.TicketId);
            ModelState.Remove("AvailableTickets");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var newTicket = await ticketService.GetById(viewModel.TicketId);
                if (viewModel.TicketId != booking.TicketId)
                {
                    if (viewModel.NewQuantity > newTicket.AvailableQuantity)
                    {
                        ModelState.AddModelError("NewQuantity", $"Not enough tickets available. Only {newTicket.AvailableQuantity} left.");
                        viewModel.AvailableTickets = await GetAvailableTicketsForEdit(viewModel.EventId, viewModel.TicketId);
                        return View(viewModel);
                    }
                    var oldTicket = await ticketService.GetById(booking.TicketId);
                    oldTicket.AvailableQuantity += booking.Amount; // Increase the available quantity of the old ticket
                    await ticketService.Update(oldTicket); // Update the old ticket's available quantity
                }
                else
                {
                    int quantityDifference = viewModel.NewQuantity - booking.Amount;
                    if (quantityDifference > newTicket.AvailableQuantity)
                    {
                        ModelState.AddModelError("NewQuantity", $"Not enough tickets available. Only {newTicket.AvailableQuantity} left.");
                        //viewModel.AvailableTickets = await GetAvailableTicketsForEdit(viewModel.EventId, viewModel.TicketId);
                        return View(viewModel);
                    }
                }
                newTicket.AvailableQuantity -= (viewModel.NewQuantity - (viewModel.TicketId == booking.TicketId ? booking.Amount : 0)); // Decrease the available quantity of the new ticket
                var result = await bookingService.UpdateAsync(viewModel, id);
                if (!result)
                {
                    ModelState.AddModelError("", "Error updating booking.");
                    //viewModel.AvailableTickets = await GetAvailableTicketsForEdit(viewModel.EventId, viewModel.TicketId);
                    return View(viewModel);
                }
                await bookingService.SaveAsync();
                return RedirectToAction("Confirmation", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating booking: {ex.Message}");
                //viewModel.AvailableTickets = await GetAvailableTicketsForEdit(viewModel.EventId, viewModel.Id);
                return View(viewModel);
            }
        }
        private async Task<List<SelectListItem>> GetAvailableTicketsForEdit(int eventId, int currentTicketId)
        {
            var availableTickets = await eventsService.GetAvailableTicketsAsync(eventId);
            var currentTicket =await ticketService.GetById(currentTicketId);
            if (currentTicket != null)
            {
                availableTickets = availableTickets
                    .Union(new List<Ticket> { currentTicket })
                    .ToList();
            }
            return availableTickets
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.TicketCategory?.Name} - {t.Price:C} (Available: {t.AvailableQuantity})",
                    Selected = t.Id == currentTicketId
                })
                .OrderBy(t => t.Text)
                .ToList();
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