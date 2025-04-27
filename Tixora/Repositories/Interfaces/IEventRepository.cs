using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IEventRepository  : IRepository<Event>
    {

        void Delete(int id);
        Event GetById(int id);
        Task<List<Event>> GetAll();
        public List<SelectListItem> GetVenues();
        public List<SelectListItem> GetOrganizers();
        Task<List<Ticket>> GetAvailableTicketsAsync(int eventId);
        List<Event> GetAll();
    }
}

