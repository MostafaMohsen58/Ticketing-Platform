using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {

        Task<List<Ticket>> GetAll();
        Task<Ticket> GetById(int id);
        Task<bool> Delete(int id);
       

        
    }
}