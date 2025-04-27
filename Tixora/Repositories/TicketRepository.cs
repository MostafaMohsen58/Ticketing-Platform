using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Tixora.Repositories
{
    public class TicketRepository: ITicketRepository
    {
         TixoraContext context;
        public TicketRepository(TixoraContext tixoracontext)
        {
            context = tixoracontext;
        }


        private IQueryable<Ticket> IncludeRelatedData()
        {
            return context.Tickets
                .Include(t => t.TicketCategory)
                .Include(t => t.Event);
                //.Include(t => t.Organizer)
                //.Include(t => t.Bookings);
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
           
            return false;

        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}











