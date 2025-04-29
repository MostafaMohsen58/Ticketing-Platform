using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tixora.Models;
using Tixora.Repositories;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;

namespace Tixora.Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IOrganizerRepository _repository;

        public OrganizerService(IOrganizerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Organizer> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<List<Organizer>> GetAll()
        {
            return await _repository.GetAll();
        }

        public List<SelectListItem> Organizers()
        {
            return _repository.GetOrganizers();
        }
        
        public async Task Add(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

            using (var scope = new TransactionScope())
            {
                try
                {
                    await _repository.AddAsync(organizer);
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task Update(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

            using (var scope = new TransactionScope())
            {
                try
                {
                    await _repository.UpdateAsync(organizer);
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    await _repository.Delete(id);
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}