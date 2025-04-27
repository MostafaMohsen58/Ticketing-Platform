using Tixora.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {

        Task<List<Ticket>> GetAll();
        Task<Ticket> GetById(int id);
        Task<bool> Delete(int id);
        IEnumerable<Ticket> GetTicketsByUser(string username);
       

    }
}
