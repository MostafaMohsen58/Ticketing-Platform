using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IOrganizerRepository : IRepository<Organizer>
    {
        Organizer GetById(int id);
        List<Organizer> GetAll();
        public List<SelectListItem> GetOrganizers();
        void Delete(int id);
    }
}
