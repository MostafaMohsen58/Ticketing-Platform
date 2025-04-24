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
            context.Tickets.Add(ticket);
        }
        public void Update(Ticket ticket)
        {
       
            context.Tickets.Update(ticket);
       
        }

        public bool Delete(int id)
        {
            var ticket = GetById(id);
            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                return true;

            }
           
            return false;

        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}











