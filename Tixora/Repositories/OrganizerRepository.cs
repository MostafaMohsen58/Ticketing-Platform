using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly TixoraContext _context;

        public OrganizerRepository(TixoraContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

           
            if (organizer.Events == null)
            {
                organizer.Events = new List<Event>();
            }

            try
            {
                await _context.Organizers.AddAsync(organizer);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding organizer: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

            try
            {
                var existingOrganizer =await GetById(organizer.Id);
                if (existingOrganizer == null)
                {
                    throw new Exception($"Organizer with ID {organizer.Id} not found");
                }

              
                existingOrganizer.Name = organizer.Name;
                existingOrganizer.ContactEmail = organizer.ContactEmail;
                existingOrganizer.ContactPhone = organizer.ContactPhone;
                existingOrganizer.LogoUrl = organizer.LogoUrl;

                
                if (organizer.Events != null)
                {
                    existingOrganizer.Events = organizer.Events;
                }

                _context.Entry(existingOrganizer).State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating organizer: {ex.Message}", ex);
            }
        }

        public async Task Delete(int id)
        {
            var organizer =await GetById(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
                //var org =await GetById(id);
                //if (org != null)
                //{
                //    _context.Organizers.Remove(org);
                //    await SaveAsync();
                //}
                await SaveAsync();
            }
        }
        public async Task<Organizer> GetById(int id)
        {
            return await _context.Organizers.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<List<Organizer>> GetAll()
        {
            return await _context.Organizers.ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Database update error: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }
        public List<SelectListItem> GetOrganizers()
        {
            return _context.Organizers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToList();
        }
    }
}