using Microsoft.AspNetCore.Mvc;
using Tixora.Services.Interfaces;
using Tixora.ViewModels.EventViewModel;

namespace Tixora.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventsService _eventsService;
        public EventController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public  IActionResult Index()
        {            
            return View( _eventsService.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            AddEventViewModel modelCopy = new AddEventViewModel()
            {
                Venues = _eventsService.Venues(),
                Organizers = _eventsService.Organizers(),
            };
            return View(modelCopy);
        }

        [HttpPost]
        public IActionResult Create(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                AddEventViewModel modelCopy = new AddEventViewModel()
                {
                    Venues = _eventsService.Venues(),
                    Organizers = _eventsService.Organizers(),
                };
                return View(modelCopy);
            }
            _eventsService.Add(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var SingleEvent = _eventsService.GetById(id);
            EditEventViewModel modelCopy = new EditEventViewModel()
            {
                Id = SingleEvent.Id,
                Title = SingleEvent.Title,
                Category = SingleEvent.Category,
                Description = SingleEvent.Description,

            };
            return View(SingleEvent);
        }
        [HttpPost]
        public IActionResult Edit(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                EditEventViewModel viewModel = new EditEventViewModel()
                {
                  Venues = _eventsService.Venues(),
                  Organizers = _eventsService.Organizers()
                };
                return View(viewModel);
            }
            _eventsService.Edit(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _eventsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
