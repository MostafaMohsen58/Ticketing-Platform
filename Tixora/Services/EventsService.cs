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

        public async Task Add(AddEventViewModel model)
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
            await _eventRepository.AddAsync(NewEvent);
            await _eventRepository.SaveAsync();
           // return Task.FromResult(NewEvent);
        }

        public async Task<bool> Delete(int id)
        {
            await _eventRepository.Delete(id);
           var n = await _eventRepository.SaveAsync();
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
             _eventRepository.UpdateAsync(e);
            return e;
        }

        public async Task<Event> GetById(int id)
        {
            return await _eventRepository.GetById(id);
        }

        public async Task<List<Event>> GetAll()
        {
            return await _eventRepository.GetAll();
        }

        public async Task<List<SelectListItem>> Venues()
        {
            return await _eventRepository.GetVenues();
        }

        public async Task<List<SelectListItem>> Organizers()
        {
            return await _eventRepository.GetOrganizers();
        }
        public async Task<List<Ticket>> GetAvailableTicketsAsync(int eventId)
        {
            var tickets = await _eventRepository.GetAvailableTicketsAsync(eventId);
            return tickets;
        }
        public async Task<List<Event>> GetUpcomingEventsAsync()
        {
            return await _eventRepository.GetUpcomingEventsAsync();
        }

    }

}
