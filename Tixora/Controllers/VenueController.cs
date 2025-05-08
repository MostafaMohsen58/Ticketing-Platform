using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Services.Interfaces;

namespace Tixora.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }
        public async Task<IActionResult> Index()
        {
            var venues =await _venueService.GetAll();
            return View(venues);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (await _venueService.CheckVenueExistWithSameName(venue.Name)!=null)
            {
                ModelState.AddModelError("", "Venue with the same name already exists.");
            }
            if (ModelState.IsValid)
            {
                await _venueService.Create(venue);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var venue =await GetVenueOrNotFound(id);
            return venue == null ? NotFound() : View(venue);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var venue =await GetVenueOrNotFound(id);
            return venue == null ? NotFound() : View(venue);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            //if (await _venueService.CheckVenueExistWithSameName(venue.Name) != null)
            //{
            //    ModelState.AddModelError("", "Venue with the same name already exists.");
            //}
            if (ModelState.IsValid)
            {
                await _venueService.Update(venue);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var venue =await GetVenueOrNotFound(id);
            return venue == null ? NotFound() : View(venue);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                await _venueService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the venue: " + ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }

        [NonAction]
        private async Task<Venue> GetVenueOrNotFound(int id)
        {
            return await _venueService.GetById(id);
        }

    }
}
