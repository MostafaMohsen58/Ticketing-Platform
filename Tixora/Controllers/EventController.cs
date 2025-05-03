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
    }
}
