using System.Collections.Generic;
using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(int id);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task<bool> DeleteAsync(int id);
    }
}
