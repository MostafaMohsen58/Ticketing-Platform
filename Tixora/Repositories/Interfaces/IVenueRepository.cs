using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        void Delete(Venue venue);
        Venue GetById(int id);
        List<Venue> GetAll();
        public List<SelectListItem> GetVenues();
    }
}
