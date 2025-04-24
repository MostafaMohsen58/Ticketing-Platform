using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {

        List<Ticket> GetAll();
        Ticket GetById(int id);
        bool Delete(int id);
       

        
    }
}