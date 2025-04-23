using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Models.ViewModels;
using Tixora.Services.Interface;
using Tixora.Services.Interfaces;



namespace Tixora.Controllers
{
    public class TicketController : Controller
    {
        ITicketService _ticketService;
        IEventService _eventService;
        ITicketCategoryService _ticketCategoryService;

        public TicketController(ITicketService ticketService, IEventService eventService, ITicketCategoryService ticketCategoryService)
        {
            _ticketService = ticketService;
            _eventService = eventService;
            _ticketCategoryService = ticketCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public async Task<IActionResult> Create()
        {
            var model = new TicketViewModel
            {
                Events = await _eventService.GetAllAsync(), 
                TicketCategories = await _ticketCategoryService.GetAllAsync() 
            };

            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    Status = model.Status,
                    Price = model.Price,
                    AvailableQuantity = model.AvailableQuantity,
                    EventId = model.EventId,
                    TicketCategoryId = model.TicketCategoryId
                };

                await _ticketService.AddAsync(ticket);

                return RedirectToAction("Index"); 
            }

            model.Events = await _eventService.GetAllAsync(); 
            model.TicketCategories = await _ticketCategoryService.GetAllAsync();

            return View(model);
        }

      

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var model = new TicketViewModel
            {
                Id = ticket.Id,
                Status = ticket.Status,
                Price = ticket.Price,
                AvailableQuantity = ticket.AvailableQuantity,
                EventId = ticket.EventId,
                TicketCategoryId = ticket.TicketCategoryId,
                Events = await _eventService.GetAllAsync(),
                TicketCategories = await _ticketCategoryService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    Id = model.Id,
                    Status = model.Status,
                    Price = model.Price,
                    AvailableQuantity = model.AvailableQuantity,
                    EventId = model.EventId,
                    TicketCategoryId = model.TicketCategoryId
                };

                await _ticketService.UpdateAsync(ticket);

                return RedirectToAction("Index");
            }

            model.Events = await _eventService.GetAllAsync();
            model.TicketCategories = await _ticketCategoryService.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket); 
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _ticketService.DeleteAsync(id);
            if (result)
            {
                return RedirectToAction("Index"); 
            }

            return NotFound();
        }

        
      
    }
}
