using System.Collections.Generic;
using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAll();
        Task<Ticket> GetById(int id);
        Task Add(Ticket ticket);
        Task Update(Ticket ticket);
        Task<bool> Delete(int id);

        IEnumerable<Ticket> GetTicketsByUser(string username);

        Task<decimal> GetTicketPriceAsync(int ticketId);


    }
}
