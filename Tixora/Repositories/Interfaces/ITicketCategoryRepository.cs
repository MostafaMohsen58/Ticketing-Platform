using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketCategoryRepository : IRepository<TicketCategory>
    {
        Task<List<TicketCategory>> GetAll();
        Task<TicketCategory> GetByID(int id);
        Task Delete(int id);
    }
}