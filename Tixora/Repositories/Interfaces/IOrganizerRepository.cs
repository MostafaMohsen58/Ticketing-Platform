using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IOrganizerRepository : IRepository<Organizer>
    {
        Organizer GetById(int id);
        List<Organizer> GetAll();
        void Delete(int id);
    }
}
