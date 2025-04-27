using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;

namespace Tixora.Services
{
    public class TicketCategoryService : ITicketCategoryService
    {
        private readonly ITicketCategoryRepository _repository;

        public TicketCategoryService(ITicketCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TicketCategory>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TicketCategory> GetById(int id)
        {
            return await _repository.GetByID(id);
        }

        public async Task Add(TicketCategory category)
        {
            if (category != null)
            {
                await _repository.AddAsync(category);
                
            }
        }

        public async Task Update(TicketCategory category)
        {
            if (category != null)
            {
                await _repository.UpdateAsync(category);
                await _repository.SaveAsync();
            }
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
            await _repository.SaveAsync();
        }
    }
}