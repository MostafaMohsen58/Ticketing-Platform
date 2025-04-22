using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IOrganizerRepository : IRepository<Organizer>
    {
        void Delete(int id);
        Organizer GetById(int id);
        List<Organizer> GetAll();
    }
}

