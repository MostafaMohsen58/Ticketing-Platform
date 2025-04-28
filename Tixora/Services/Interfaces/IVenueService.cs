using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;
using Tixora.ViewModels;

namespace Tixora.Services.Interfaces
{
    public interface IVenueService
    {
        Task<List<Venue>> GetAll();
        Task<Venue> GetById(int id);
        Task Create(Venue venue);
        Task Update(Venue venue);
        Task Delete(int id);
        public Task<Venue?> CheckVenueExistWithSameName(string name);
        List<SelectListItem> Venues();
    }
}
