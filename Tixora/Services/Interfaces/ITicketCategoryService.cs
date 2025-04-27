using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface ITicketCategoryService
    {
        Task<List<TicketCategory>> GetAll();
        Task<TicketCategory> GetById(int id);
        Task Add(TicketCategory category);
        Task Update(TicketCategory category);
        Task Delete(int id);
    }
}
