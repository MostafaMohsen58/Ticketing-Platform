using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface ITicketCategoryService
    {
        List<TicketCategory> GetAll();
        TicketCategory GetById(int id);
        void Add(TicketCategory category);
        void Update(TicketCategory category);
        void Delete(int id);
    }
}
