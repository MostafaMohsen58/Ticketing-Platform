using System.Collections.Generic;
using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface ITicketService
    {
        List<Ticket> GetAll();
        Ticket GetById(int id);
        void Add(Ticket ticket);
        void Update(Ticket ticket);
        bool Delete(int id);

        IEnumerable<Ticket> GetTicketsByUser(string username);




    }
}
