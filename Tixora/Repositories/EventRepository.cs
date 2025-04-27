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
        public  void Add(Event obj)
        {
            _context.Events.Add(obj);
        }
        public async Task Update(Event obj)
        {
            _context.Events.Update(obj);
        }

        public async Task  Delete(int id)
        {
            var Event =await GetById(id);
            if (Event != null)
            {
                 _context.Events.Remove(Event);
            }
        }
        public async Task<Event> GetById(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(o => o.Id == id)!;
        }
        public  List<Event> GetAll()
        {
            return  _context.Events.ToList();
        }

        public int Save()
        {
            return  _context.SaveChanges();
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
    }
}
