using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;
using Tixora.ViewModels;

namespace Tixora.Services.Interfaces
{
    public interface IVenueService
    {
        List<Venue> GetAll();
        List<SelectListItem> Venues();
        Venue GetById(int id);
        void Create(Venue venue);
        void Update(Venue venue);
        void Delete(int id);
        public Venue? CheckVenueExistWithSameName(string name);
    }
}
