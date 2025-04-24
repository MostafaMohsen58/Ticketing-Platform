using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IEventRepository 
    {
        Task Add(Event obj);
        void Update(Event obj);
        Task<int> Save();
        void Delete(int id);
        Event GetById(int id);
        Task<List<Event>> GetAll();
        public List<SelectListItem> GetVenues();
        public List<SelectListItem> GetOrganizers();

    }
}

