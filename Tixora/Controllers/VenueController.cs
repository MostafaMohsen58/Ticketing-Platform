using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Services.Interfaces;

namespace Tixora.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }
        public IActionResult Index()
        {
            var venues = _venueService.GetAll();
            return View(venues);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Venue venue)
        {
            if (_venueService.CheckVenueExistWithSameName(venue.Name)!=null)
            {
                ModelState.AddModelError("", "Venue with the same name already exists.");
            }
            if (ModelState.IsValid)
            {
                _venueService.Create(venue);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
        
        public IActionResult Details(int id)
        {
            var venue = GetVenueOrNotFound(id);
            return venue == null ? NotFound() : View(venue);
        }

        public IActionResult Edit(int id)
        {
            var venue = GetVenueOrNotFound(id);
            return venue == null ? NotFound() : View(venue);
        }
        [HttpPost]
        public IActionResult Edit(int id, Venue venue)
        {
            if (_venueService.CheckVenueExistWithSameName(venue.Name) != null)
            {
                ModelState.AddModelError("", "Venue with the same name already exists.");
            }
            if (ModelState.IsValid)
            {
                _venueService.Update(venue);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var venue = GetVenueOrNotFound(id);
            return venue == null ? NotFound() : View(venue);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _venueService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private Venue GetVenueOrNotFound(int id)
        {
            return _venueService.GetById(id);
        }

    }
}
