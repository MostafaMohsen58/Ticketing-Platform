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
        Event GetById(int id);
        Event Edit(EditEventViewModel model);
        bool Delete(int id);
        List<SelectListItem> Venues();
        List<SelectListItem> Organizers();

    }

}
