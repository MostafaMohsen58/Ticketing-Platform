using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IEventRepository  : IRepository<Event>
    {
        Task Add(Event obj);
        Task Update(Event obj);
        Task<int> Save();
        Task Delete(int id);
        Task<Event> GetById(int id);
        Task<List<Event>> GetAll();
        public Task<List<SelectListItem>> GetVenues();
        public Task<List<SelectListItem>> GetOrganizers();
        Task<List<Ticket>> GetAvailableTicketsAsync(int eventId);
    }
}

