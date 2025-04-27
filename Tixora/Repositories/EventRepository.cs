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
        public  List<Event> GetAll()
        {
            return  _context.Events.ToList();
        }

        public int Save()
        {
            return  _context.SaveChanges();
        }
        public List<SelectListItem> GetOrganizers()
        {
            return _context.Organizers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(x => x.Text).AsNoTracking().ToList();
        }
        public async Task<List<Ticket>> GetAvailableTicketsAsync(int eventId)
        {
            return await _context.Tickets
                .Where(t => t.EventId == eventId && t.AvailableQuantity > 0 && t.Status == 0)
                .ToListAsync();
        }
        
    }
}
