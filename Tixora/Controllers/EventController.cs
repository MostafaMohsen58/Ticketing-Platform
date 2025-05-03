using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Services;
using Tixora.Services.Interfaces;
using Tixora.ViewModels.EventViewModel;
namespace Tixora.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventsService _eventsService;
        private readonly IVenueService _venueService;
        private readonly IOrganizerService _organizerService;
        public EventController(IEventsService eventsService, IVenueService venueService, IOrganizerService organizerService)
        {
            _eventsService = eventsService;
            _venueService = venueService;
            _organizerService = organizerService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _eventsService.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AddEventViewModel modelCopy = new AddEventViewModel()
            {
                Venues = await _eventsService.Venues(),
                Organizers = await _eventsService.Organizers(),
            };
            return View(modelCopy);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                AddEventViewModel modelCopy = new AddEventViewModel()
                {
                    Venues = await _eventsService.Venues(),
                    Organizers = await _eventsService.Organizers(),
                };
                return View(modelCopy);
            }
            await _eventsService.Add(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var SingleEvent = await _eventsService.GetById(id);
            EditEventViewModel modelCopy = new EditEventViewModel()
            {
                Id = SingleEvent!.Id,
                Title = SingleEvent.Title,
                Curuntcover = SingleEvent.ImageUrl,
                Category = SingleEvent.Category,
                Description = SingleEvent.Description,
                VenueId = SingleEvent.VenueId,
                OrganizerId = SingleEvent.OrganizerId,
                Organizers = _organizerService.Organizers(),
                Venues = _venueService.Venues(),
            };

            return View(modelCopy);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                EditEventViewModel viewModel = new EditEventViewModel()
                {
                    Venues = await _eventsService.Venues(),
                    Organizers = await _eventsService.Organizers()
                };
                return View(viewModel);
            }
            await _eventsService.Edit(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Event = await _eventsService.GetById(id);
            return View(Event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _eventsService.GetById(id));
        }
        
        

        [HttpGet]
        public async Task<IActionResult> Explore()
        {
            var events = await _eventsService.GetAll();

            return View(events);
        }
        [HttpPost]
        public async Task<IActionResult> Explore(DateTime? startDate, string? location, string? search)
        {
            var events = await Search(startDate, location, search, "");

            return View(events);
        }
        [HttpGet]
        public async Task<IActionResult> ExploreEntertainment(DateTime? startDate, string? location, string? search)
        {
            string category = "entertainment";
            var events = await Search(startDate, location, search, category);

            return View("Entertainment",events);    
        }

        [HttpGet]
        public async Task<IActionResult> ExploreMatches(DateTime? startDate, string? location, string? search)
        {
            string category = "Matches";

            var events = await Search(startDate, location, search,category);

            return View("Matches",events);
        }

        [HttpGet]
        public async Task<IActionResult> Matches(string category = null, int? venueId = null, DateTime? date = null, string search = null)
        {
            // Get all events
            var events = await _eventsService.GetAll();

            // Filter by category (like "matches", "sports", etc)

            events = events.Where(e => e.Category.Equals("Matches", StringComparison.OrdinalIgnoreCase)).ToList();

            
            return View(events);
        }
        [HttpGet]
        public async Task<IActionResult> Entertainment( DateTime? date = null, string search = null)
        {
            
            // Get all events
            var events = await _eventsService.GetAll();
            events = events.Where(e => e.Category.Equals("entertainment", StringComparison.OrdinalIgnoreCase)).ToList();

            return View(events);
        }
        [NonAction]
        private async Task<List<Event>> Search(DateTime? startDate, string? location, string? search, string category)
        {
            var events = await _eventsService.GetAll();

            if (category != "")
            {
                events = events.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (startDate.HasValue)
            {
                events = events.Where(e => e.StartDate >= startDate.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                events = events.Where(e => e.Venue != null && e.Venue.Name.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                events = events.Where(e =>
                    (!string.IsNullOrEmpty(e.Title) && e.Title.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.Description) && e.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return events;
        }
    }
}
