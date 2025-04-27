using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TixoraContext _context;

        public TicketRepository(TixoraContext context)
        {
            _context = context;
        }

        private IQueryable<Ticket> IncludeRelatedData()
        {
            return _context.Tickets
                .Include(t => t.TicketCategory)
                .Include(t => t.Event);
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await IncludeRelatedData().ToListAsync();
            
        }

        public async Task<Ticket> GetById(int id)
        {
            return await IncludeRelatedData().FirstOrDefaultAsync(t => t.Id == id);
           
        }

        public async Task AddAsync(Ticket ticket)
        {
            await context.Tickets.AddAsync(ticket);
        }
        public async Task UpdateAsync(Ticket ticket)
        {
       
             context.Tickets.Update(ticket);
        }

        public async Task<bool> Delete(int id)
        {
            var ticket =await GetById(id);
            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                return true;

            }
           
                var ticket = GetById(id);
                if (ticket != null)
                {
                    _context.Tickets.Remove(ticket);
                    return true;
                }
                return false;
          
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        public IEnumerable<Ticket> GetTicketsByUser(string username)
        {
            return _context.Bookings
                .Include(b => b.Ticket)
                    .ThenInclude(t => t.Event)
                .Include(b => b.Ticket)
                    .ThenInclude(t => t.TicketCategory)
                .Include(b => b.User)
                .Where(b => b.User.UserName == username)
                .Select(b => b.Ticket)
                .Distinct()
                .ToList();
        }

    }
}
