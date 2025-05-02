using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Explore(DateTime? startDate, string? location, string? search)
        {
            var events = await _eventsService.GetAll();

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

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Matches(string category = null, int? venueId = null, DateTime? date = null, string search = null)
        {
            // Get all events
            var events = await _eventsService.GetAll();

            // Filter by category (like "matches", "sports", etc)
            
             events = events.Where(e => e.Category.Equals("matches", StringComparison.OrdinalIgnoreCase)).ToList();
            

            // Filter by venue
            if (venueId.HasValue)
            {
                events = events.Where(e => e.VenueId == venueId.Value).ToList();
            }

            // Filter by date
            if (date.HasValue)
            {
                events = events.Where(e => e.StartDate.Date == date.Value.Date).ToList();
            }

            // Filter by search term
            if (!string.IsNullOrEmpty(search))
            {
                events = events.Where(e =>
                    e.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Get venues for filter dropdown
            var venues = await _venueService.GetAll();

            // Prepare view model
            var viewModel = new MatchesViewModel
            {
                Events = events,
                Venues = venues,
                SelectedCategory = category,
                SelectedVenueId = venueId,
                SelectedDate = date,
                SearchTerm = search
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Entertainment(int? venueId = null, DateTime? date = null, string search = null)
        {
            // Get all events
            var events = await _eventsService.GetAll();

            // Filter to only show entertainment events
            events = events.Where(e => e.Category.Equals("entertainment", StringComparison.OrdinalIgnoreCase)).ToList();

            // Filter by venue
            if (venueId.HasValue)
            {
                events = events.Where(e => e.VenueId == venueId.Value).ToList();
            }

            // Filter by date
            if (date.HasValue)
            {
                events = events.Where(e => e.StartDate.Date == date.Value.Date).ToList();
            }

            // Filter by search term
            if (!string.IsNullOrEmpty(search))
            {
                events = events.Where(e =>
                    e.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Get venues for filter dropdown
            var venues = await _venueService.GetAll();

            // Prepare view model
            var viewModel = new EntertainmentViewModel
            {
                Events = events,
                Venues = venues,
                SelectedVenueId = venueId,
                SelectedDate = date,
                SearchTerm = search
            };

            return View(viewModel);
        }

    }
}
