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

        public Organizer GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Organizer> GetAll()
        {
            return _repository.GetAll();
        }
        public List<SelectListItem> Organizers()
        {
            return _repository.GetOrganizers();
        }
        public void Add(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

            using (var scope = new TransactionScope())
            {
                try
                {
                    _repository.Add(organizer);
                    scope.Complete();
                }
                catch (Exception)
                {
                    // Transaction will automatically be rolled back
                    throw;
                }
            }
        }

        public void Update(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

            using (var scope = new TransactionScope())
            {
                try
                {
                    _repository.Update(organizer);
                    scope.Complete();
                }
                catch (Exception)
                {
                    // Transaction will automatically be rolled back
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    _repository.Delete(id);
                    scope.Complete();
                }
                catch (Exception)
                {
                    // Transaction will automatically be rolled back
                    throw;
                }
            }
        }
    }
}