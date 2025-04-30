using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tixora.Models;
using Tixora.ViewModels.EventViewModel;

namespace Tixora.Services.Interfaces
{
    public interface IEventsService
    {
        Task<List<Event>> GetAll();
        Task Add(AddEventViewModel model);
        Task<Event> GetById(int id);
        Event Edit(EditEventViewModel model);
        Task<bool> Delete(int id);
        Task<List<SelectListItem>> Venues();
        Task<List<SelectListItem>> Organizers();
        Task<List<Ticket>> GetAvailableTicketsAsync(int eventId);
        Task<List<Event>> GetUpcomingEventsAsync();

    }

}
