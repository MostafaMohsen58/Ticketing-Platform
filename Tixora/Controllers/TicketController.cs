using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Models.ViewModels;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;



namespace Tixora.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketController : Controller
    {
        ITicketService _ticketService;
        IEventsService _eventService;
        ITicketCategoryService _ticketCategoryService;

        public TicketController(ITicketService ticketService, IEventsService eventService, ITicketCategoryService ticketCategoryService)
        {
            _ticketService = ticketService;
            _eventService = eventService;
            _ticketCategoryService = ticketCategoryService;
        }

        public async Task<IActionResult> Index(float? price, TicketStatus? status)
        {
           
            var tickets = await _ticketService.GetAll();

            var viewModel = new TicketIndexViewModel
            {
                Price = price,
                Status = status,
                Tickets = tickets
            };

          
            return View(viewModel);
        }




        public async Task<IActionResult> Details(int id)
        {
            var ticket =await  _ticketService.GetById(id);
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
                Events =await _eventService.GetAll(),
                TicketCategories =await _ticketCategoryService.GetAll()
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
                    Price = model.Price,
                    AvailableQuantity = model.AvailableQuantity,
                    EventId = model.EventId,
                    TicketCategoryId = model.TicketCategoryId
                };

                 await _ticketService.Add(ticket);

                return RedirectToAction("Index"); 
            }

            model.Events = await _eventService.GetAll(); 
            model.TicketCategories = await _ticketCategoryService.GetAll();

            return View(model);
        }

      

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var model = new TicketViewModel
            {
                Id = ticket.Id,
                //Status = ticket.Status,
                Price = ticket.Price,
                AvailableQuantity = ticket.AvailableQuantity,
                EventId = ticket.EventId,
                TicketCategoryId = ticket.TicketCategoryId,
                Events =await _eventService.GetAll(),
                TicketCategories = await _ticketCategoryService.GetAll()
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
                    //Status = model.Status,
                    Price = model.Price,
                    AvailableQuantity = model.AvailableQuantity,
                    EventId = model.EventId,
                    TicketCategoryId = model.TicketCategoryId
                };

                 await _ticketService.Update(ticket);

                return RedirectToAction("Index");
            }

            model.Events = await _eventService.GetAll();
            model.TicketCategories = await _ticketCategoryService.GetAll();

            return View(model);
        }

    
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var ticket =await _ticketService.GetById(id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ticket);
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _ticketService.Delete(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Ticket successfully deleted.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete ticket. The ticket may not exist or has already been deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log exception if you have logging configured
                TempData["ErrorMessage"] = $"An error occurred while deleting the ticket: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "User")]
        public IActionResult History()
        {
            var userTickets = _ticketService.GetTicketsByUser(User.Identity.Name);
            return View(userTickets);
        }

    }
}
