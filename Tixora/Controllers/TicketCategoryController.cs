using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Services.Interfaces;

namespace Tixora.Controllers
{
    public class TicketCategoryController : Controller
    {
        private readonly ITicketCategoryService _service;

        public TicketCategoryController(ITicketCategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _service.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCategory category)
        {
            ModelState.Remove("Tickets");
            if (ModelState.IsValid)
            {
                try
                {
                    if (category.Tickets == null)
                    {
                        category.Tickets = new List<Ticket>();
                    }
                    await _service.Add(category);
                    TempData["SuccessMessage"] = "Category created successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TicketCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(category);
                    TempData["SuccessMessage"] = "Ticket category updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.Delete(id);
                TempData["SuccessMessage"] = "Ticket category has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
