using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels.EventViewModel;

namespace Tixora.Services
{
    
    public class EventsService : IEventsService 
    {

        private readonly FileService _fileService;
        private readonly IEventRepository _eventRepository;
        public EventsService(IEventRepository eventRepository, FileService fileService)
        {
            _fileService = fileService;
            _eventRepository = eventRepository;
        }

        public async Task Add(AddEventViewModel model)
        {
            var Covername = await _fileService.SaveCoverAsync(model.Cover);
            var NewEvent = new Event()
            {
                Category = model.Category,
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                ImageUrl = Covername,
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


        public async Task<Event> Edit(EditEventViewModel model)
        {
            var ev = await GetById(model.Id!.Value);

            if (ev == null)
            {
                return null!;
            }
            var newimg = model.Cover != null;
            var oldimg = ev.ImageUrl;
            if (oldimg == null) return null!;


            ev.Category = model.Category;
            ev.Title = model.Title;
            ev.Description = model.Description;
            ev.StartDate = model.StartDate;
            ev.VenueId = model.VenueId;
            ev.OrganizerId = model.OrganizerId;

            if (newimg)
            {
                ev.ImageUrl = await _fileService.SaveCoverAsync(model.Cover!);
            }
            int ER = await _eventRepository.SaveAsync();
            if (ER > 0)
            {
                if (newimg)
                {
                    _fileService.DeleteCover(oldimg);
                }
                return ev;
            }
            else
            {
                _fileService.DeleteCover(ev.ImageUrl);
                return null!;
            }
        }




        public async Task<Event?> GetById(int id)
        {
            return await _eventRepository.GetById(id)!;
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
    }

}
