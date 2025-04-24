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

        public List<TicketCategory> GetAll()
        {
            return _repository.GetAll();
        }

        public TicketCategory GetById(int id)
        {
            return _repository.GetByID(id);
        }

        public void Add(TicketCategory category)
        {
            if (category != null)
            {
                _repository.Add(category);
                
            }
        }

        public void Update(TicketCategory category)
        {
            if (category != null)
            {
                _repository.Update(category);
                _repository.Save();
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }
    }
}