using Tixora.Models;

namespace Tixora.Services.Interfaces
{
    public interface IOrganizerService
    {
        Organizer GetById(int id);
        List<Organizer> GetAll();
        void Add(Organizer organizer);
        void Update(Organizer organizer);
        void Delete(int id);
    }
}
