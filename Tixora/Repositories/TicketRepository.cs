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

        public List<Ticket> GetAll()
        {
                return IncludeRelatedData().ToList();
        
        }

        public Ticket GetById(int id)
        {
        
                return IncludeRelatedData().FirstOrDefault(t => t.Id == id);
        }

        public void Add(Ticket ticket)
        {
           
                _context.Tickets.Add(ticket);
         
        }

        public void Update(Ticket ticket)
        {
           
                _context.Tickets.Update(ticket);
          
        }

        public bool Delete(int id)
        {
           
                var ticket = GetById(id);
                if (ticket != null)
                {
                    _context.Tickets.Remove(ticket);
                    return true;
                }
                return false;
          
        }

        public int Save()
        {
        
                return _context.SaveChanges();
        
          
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
