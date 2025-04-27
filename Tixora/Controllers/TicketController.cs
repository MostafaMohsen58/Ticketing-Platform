using Microsoft.AspNetCore.Mvc;
using Tixora.Models;
using Tixora.Models.ViewModels;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;



namespace Tixora.Controllers
{
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
            var tickets =await _ticketService.GetAll();

            if (price.HasValue)
            {
                tickets = tickets.Where(t => t.Price == price).ToList();
            }

            if (status.HasValue)
            {
                TicketStatus convertedStatus = (TicketStatus)status.Value;
                tickets = tickets.Where(t => t.Status == convertedStatus).ToList();
            }

            var availableCount = tickets.Count(t => t.Status == TicketStatus.Available);
            var soldOutCount = tickets.Count(t => t.Status == TicketStatus.NonAvailable);

            var viewModel = new TicketIndexViewModel
            {
                Price = price,
                Status = status,
                Tickets = tickets,
                AvailableCount = availableCount,
                SoldOutCount = soldOutCount
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
                    Status = model.Status,
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
                Status = ticket.Status,
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
                    Status = model.Status,
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

    
        public async Task<IActionResult> Delete(int id)
        {
            var ticket =await _ticketService.GetById(id);
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
            var result =await _ticketService.Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> Report()
        {
            
            var tickets = await _ticketService.GetAll();

            var totalRevenue = tickets.Sum(t => t.Price);
            var totalSold = tickets.Count(t => t.Status == TicketStatus.NonAvailable);

           
            var reportViewModel = new ReportViewModel
            {
                TotalRevenue = totalRevenue,
                TotalSold = totalSold
            };

            
            return View(reportViewModel);
        }

        public IActionResult History()
        {
            var userTickets = _ticketService.GetTicketsByUser(User.Identity.Name);
            return View(userTickets);
        }

    }
}
