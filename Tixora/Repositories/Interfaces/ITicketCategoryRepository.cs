using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketCategoryRepository : IRepository<TicketCategory>
    {
        List<TicketCategory> GetAll();
        TicketCategory GetByID(int id);
        void Delete(int id);
    }
}