using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels.EventViewModel;

namespace Tixora.Services
{
    
    public class EventsService : IEventsService 
    {
        
        private readonly IEventRepository _eventRepository;
        public EventsService( IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public  void Add(AddEventViewModel model)
        {
            var NewEvent = new Event()
            {
                Category = model.Category,
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                VenueId = model.VenueId,
                OrganizerId = model.OrganizerId,
            };
             _eventRepository.Add(NewEvent);
             _eventRepository.Save();
           // return Task.FromResult(NewEvent);
        }

        public  bool Delete(int id)
        {
            _eventRepository.Delete(id);
            _eventRepository.Save();
            return true;
        }

        public Event Edit(EditEventViewModel model)
        {
            Event e = new Event()
            {
                Category = model.Category,
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                VenueId = model.VenueId,
                OrganizerId = model.OrganizerId,

            };
             _eventRepository.Update(e);
            return e;
        }

        public Event GetById(int id)
        {
            return _eventRepository.GetById(id);
        }

        public  List<Event> GetAll()
        {
            return  _eventRepository.GetAll();
        }

     
        public List<SelectListItem> Organizers()
        {
            return _eventRepository.GetOrganizers();
        }
        public async Task<List<Ticket>> GetAvailableTicketsAsync(int eventId)
        {
            var tickets = await _eventRepository.GetAvailableTicketsAsync(eventId);
            return tickets;
        }
    }

}
