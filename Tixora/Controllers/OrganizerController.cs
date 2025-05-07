using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Tixora.Models;
using Tixora.Services.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Http;

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
            var organizers = await _service.GetAll();
            return View(organizers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var org = await _service.GetById(id);
            if (org == null) return NotFound();
            return View(org);
        }

        public async Task<IActionResult> Create()
        {
            return View(new Organizer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Organizer organizer, IFormFile file)
        {
            if (ModelState.ContainsKey("Events"))
            {
                ModelState.Remove("Events");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        organizer.LogoUrl = UploadFile(file);
                    }

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
            var organizer = await _service.GetById(id);
            if (organizer == null) return NotFound();
            return View(organizer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Organizer organizer, IFormFile? file)
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
                    var existingOrganizer = await _service.GetById(id);
                    if (existingOrganizer != null )
                    {
                        if (file != null && file.Length > 0)
                        {
                            organizer.LogoUrl = UploadFile(file);
                        }

                        await _service.Update(organizer);
                        TempData["SuccessMessage"] = "Organizer updated successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Organizer not found.");
                    }


                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }
            return View(organizer);
        }

        // public async Task<IActionResult> Delete(int id)
        // {
        //     var org = await _service.GetById(id);
        //     if (org == null) return NotFound();
        //     return View(org);
        // }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                TempData["SuccessMessage"] = "Organizer deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        private string? UploadFile(IFormFile ImageUrl)
        {
            string filePath = null;
            string uniqueFileName = null;

            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/user");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageUrl.FileName);
                filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageUrl.CopyTo(fileStream);
                }

                return uniqueFileName;
            }

            return null;
        }
    }
}