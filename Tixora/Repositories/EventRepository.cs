using Tixora.Models.Context;
using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tixora.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TixoraContext _context;
        public EventRepository(TixoraContext context)
        {
            _context = context;
        }
        public async Task Add(Event obj)
        {
           await _context.Events.AddAsync(obj);
        }
        public void Update(Event obj)
        {
            _context.Events.Update(obj);
        }

        public void  Delete(int id)
        {
            var Event = GetById(id);
            if (Event != null)
            {
                 _context.Events.Remove(Event);
            }
        }
        public Event GetById(int id)
        {
            return _context.Events.FirstOrDefault(o => o.Id == id)!;
        }
        public async Task<List<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public List<SelectListItem> GetVenues()
        {
            return _context.Venues.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToList();
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
