using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly TixoraContext _context;
        public VenueRepository(TixoraContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Venue obj)
        {
            await _context.Venues.AddAsync(obj);
        }

        public async Task UpdateAsync(Venue obj)
        {
            _context.Venues.Update(obj);
        }

        public void Delete(Venue venue)
        {
            _context.Venues.Remove(venue);
        }

        public async Task<Venue> GetById(int id)
        {
            return await _context.Venues.FirstOrDefaultAsync(v => v.Id == id);                
        }

        public async Task<List<Venue>> GetAll()
        {
            return await _context.Venues.ToListAsync();
        }
        public List<SelectListItem> GetVenues()
        {
            return _context.Venues.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToList();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
