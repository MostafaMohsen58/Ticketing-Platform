using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IEventRepository  : IRepository<Event>
    {

        void Delete(int id);
        Event GetById(int id);
        List<Event> GetAll();
        
      


    }
}

