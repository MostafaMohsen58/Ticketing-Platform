using Tixora.Models.Context;
using Tixora.Models;
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
        public void Add(Organizer obj)
        {
            _context.Organizers.Add(obj);
        }
        public void Update(Organizer obj)
        {
            _context.Organizers.Update(obj);
        }
        public void Delete(int id)
        {
            var organizer = GetById(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
            }
        }
        public Organizer GetById(int id)
        {
            return _context.Organizers.FirstOrDefault(o => o.Id == id);
        }
        public List<Organizer> GetAll()
        {
            return _context.Organizers.ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
