using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface IOrganizerService
    {
        Task<Organizer> GetById(int id);
        Task<List<Organizer>> GetAll();
        Task Add(Organizer organizer);
        Task Update(Organizer organizer);
        Task Delete(int id);
        List<SelectListItem> Organizers();
    }
}
