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
        public void Add(Venue obj)
        {
            _context.Venues.Add(obj);
        }

        public void Update(Venue obj)
        {
            _context.Venues.Update(obj);
        }

        public void Delete(Venue venue)
        {
                _context.Venues.Remove(venue);
        }

        public Venue GetById(int id)
        {
            return _context.Venues.FirstOrDefault(v => v.Id == id)!;                
        }

        public List<Venue> GetAll()
        {
            return _context.Venues.ToList();
        }
        public List<SelectListItem> GetVenues()
        {
            return _context.Venues.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToList();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
