using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Tixora.Models;
using Tixora.Services.Interfaces;

namespace Tixora.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _service;

        public OrganizerController(IOrganizerService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var organizers =await _service.GetAll();
            return View(organizers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var org =await _service.GetById(id);
            if (org == null) return NotFound();
            return View(org);
        }

        public async Task<IActionResult> Create()
        {
            return View(new Organizer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Organizer organizer)
        {
           
            if (ModelState.ContainsKey("Events"))
            {
                ModelState.Remove("Events");
            }

            if (ModelState.IsValid)
            {
                try
                {
                  
                    if (organizer.Events == null)
                    {
                        organizer.Events = new List<Event>();
                    }

                    await _service.Add(organizer);
                    TempData["SuccessMessage"] = "Organizer created successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }
            return View(organizer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var organizer =await _service.GetById(id);
            if (organizer == null) return NotFound();
            return View(organizer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Organizer organizer)
        {
            if (id != organizer.Id)
            {
                return NotFound();
            }

            if (ModelState.ContainsKey("Events"))
            {
                ModelState.Remove("Events");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrganizer =await _service.GetById(id);
                    if (existingOrganizer != null && existingOrganizer.Events != null)
                    {
                        organizer.Events = existingOrganizer.Events;
                    }
                    else
                    {
                        organizer.Events = new List<Event>();
                    }

                    await _service.Update(organizer);
                    TempData["SuccessMessage"] = "Organizer updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }
            return View(organizer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var org =await _service.GetById(id);
            if (org == null) return NotFound();
            return View(org);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.Delete(id);
                TempData["SuccessMessage"] = "Organizer deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}