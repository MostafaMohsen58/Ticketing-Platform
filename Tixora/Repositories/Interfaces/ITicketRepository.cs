using Tixora.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        List<Ticket> GetAll();
        Ticket GetById(int id);
        bool Delete(int id);
        IEnumerable<Ticket> GetTicketsByUser(string username);



    }
}
