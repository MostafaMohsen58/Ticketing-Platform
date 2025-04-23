using Tixora.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
