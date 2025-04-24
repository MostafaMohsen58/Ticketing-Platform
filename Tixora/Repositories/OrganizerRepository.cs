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

        public Organizer GetById(int id)
        {
            return _context.Organizers.Include(o => o.Events).FirstOrDefault(o => o.Id == id);
        }

        public List<Organizer> GetAll()
        {
            return _context.Organizers.Include(o => o.Events).ToList();
        }

        public void Add(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

           
            if (organizer.Events == null)
            {
                organizer.Events = new List<Event>();
            }

            try
            {
                _context.Organizers.Add(organizer);
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding organizer: {ex.Message}", ex);
            }
        }

        public void Update(Organizer organizer)
        {
            if (organizer == null)
                throw new ArgumentNullException(nameof(organizer));

            try
            {
                var existingOrganizer = GetById(organizer.Id);
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
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating organizer: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var org = GetById(id);
                if (org != null)
                {
                    _context.Organizers.Remove(org);
                    Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting organizer: {ex.Message}", ex);
            }
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Database update error: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }
    }
}