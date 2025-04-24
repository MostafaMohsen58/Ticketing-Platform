using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IEventRepository 
    {
        void Add(Event obj);
        void Update(Event obj);
        int Save();
        void Delete(int id);
        Event GetById(int id);
        List<Event> GetAll();
        public List<SelectListItem> GetVenues();
        public List<SelectListItem> GetOrganizers();

    }
}

