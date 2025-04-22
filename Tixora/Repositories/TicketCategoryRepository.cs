using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        TixoraContext context;
        public TicketCategoryRepository(TixoraContext db) { 
        
            context = db;
        }
        public List<TicketCategory> GetAll()
        {
            List<TicketCategory> ticketCategories = context.TicketCategories.ToList();
            return ticketCategories;
        }

        public TicketCategory GetByID(int id)
        {
            TicketCategory ticketCategory = context.TicketCategories.FirstOrDefault(c => c.Id == id);
            return ticketCategory;
        }
        public void Add(TicketCategory ticketCategory)
        {
            context.TicketCategories.Add(ticketCategory);
           
        }

        public void Update(int id, TicketCategory newTicketCategory)
        {
            TicketCategory oldTicketCategory = GetByID(id);
            oldTicketCategory.Name = newTicketCategory.Name;
            oldTicketCategory.Description = newTicketCategory.Description;
            oldTicketCategory.PriceMultiplier = newTicketCategory.PriceMultiplier;
            

        }

        public void Delete(int id) {

            TicketCategory ticketCategory = GetByID(id);
            context.TicketCategories.Remove(ticketCategory);
            

        }

        public void Update(TicketCategory obj)
        {
            throw new NotImplementedException();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
