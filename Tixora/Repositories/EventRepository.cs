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
        public async Task AddAsync(Event obj)
        {
            await _context.Events.AddAsync(obj);
        }
        public async Task UpdateAsync(Event obj)
        {
            _context.Events.Update(obj);
        }

        public async Task Delete(int id)
        {
            var Event = await GetById(id);
            if (Event != null)
            {
                _context.Events.Remove(Event);
            }
        }
        public async Task<Event?> GetById(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(o => o.Id == id)!;
        }
        public async Task<List<Event>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            var n = await _context.SaveChangesAsync();
            return n;
        }

        public async Task<List<SelectListItem>> GetVenues()
        {
            return await _context.Venues.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToListAsync();
        }

        public async Task<List<SelectListItem>> GetOrganizers()
        {
            return await _context.Organizers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToListAsync();
        }
        public async Task<List<Ticket>> GetAvailableTicketsAsync(int eventId)
        {
            return await _context.Tickets
                .Where(t => t.EventId == eventId && t.AvailableQuantity > 0 && t.Status == 0)
                .ToListAsync();
        }
        public async Task<List<Event>> GetUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.StartDate >= DateTime.Now)
                .OrderBy(e => e.StartDate)
                .ToListAsync();
        }


    }
}
