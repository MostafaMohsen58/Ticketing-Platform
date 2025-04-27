using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TixoraContext context;

        public TicketCategoryRepository(TixoraContext db)
        {
            context = db;
        }

        public List<TicketCategory> GetAll()
        {
            return context.TicketCategories.ToList();
        }

        public TicketCategory GetByID(int id)
        {
            return context.TicketCategories.FirstOrDefault(c => c.Id == id);
        }

        public async Task AddAsync(TicketCategory ticketCategory)
        {
            context.TicketCategories.AddAsync(ticketCategory);
            await SaveAsync(); 
        }

        public async Task UpdateAsync(TicketCategory ticketCategory)
        {
            var existingCategory = GetByID(ticketCategory.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = ticketCategory.Name;
                existingCategory.Description = ticketCategory.Description;
                existingCategory.PriceMultiplier = ticketCategory.PriceMultiplier;
                context.Entry(existingCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var ticketCategory = GetByID(id);
            if (ticketCategory != null)
            {
                context.TicketCategories.Remove(ticketCategory);
            }
        }
        public async Task<int> SaveAsync()
        {
            return context.SaveChanges();
        }
    }
}