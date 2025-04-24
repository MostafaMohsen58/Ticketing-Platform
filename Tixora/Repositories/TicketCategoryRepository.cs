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
<<<<<<< Updated upstream
        public void Add(TicketCategory ticketCategory)
        {
            context.TicketCategories.Add(ticketCategory);
            Save(); 
        }

        public void Update(int id, TicketCategory newTicketCategory)
=======

        public async Task AddAsync(TicketCategory ticketCategory)
        {
            context.TicketCategories.AddAsync(ticketCategory);
            SaveAsync(); 
        }

        public async Task UpdateAsync(TicketCategory ticketCategory)
>>>>>>> Stashed changes
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

        public void Update(TicketCategory obj)
        {
            throw new NotImplementedException();
        }
<<<<<<< Updated upstream
        public int Save()
=======

        public async Task<int> SaveAsync()
>>>>>>> Stashed changes
        {
            return context.SaveChanges();
        }
    }
}