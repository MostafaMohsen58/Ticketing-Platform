using Microsoft.EntityFrameworkCore;
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

        public async Task<List<TicketCategory>> GetAll()
        {
            return await context.TicketCategories.ToListAsync();
        }

        public async Task<TicketCategory> GetByID(int id)
        {
            return await context.TicketCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(TicketCategory ticketCategory)
        {
            await context.TicketCategories.AddAsync(ticketCategory);
            await SaveAsync(); 
        }

        public async Task UpdateAsync(TicketCategory ticketCategory)
        {
            var existingCategory =await GetByID(ticketCategory.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = ticketCategory.Name;
                existingCategory.Description = ticketCategory.Description;
                existingCategory.PriceMultiplier = ticketCategory.PriceMultiplier;
                context.Entry(existingCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }

        public async Task Delete(int id)
        {
            var ticketCategory =await GetByID(id);
            if (ticketCategory != null)
            {
                 context.TicketCategories.Remove(ticketCategory);
            }
        }
        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}