using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        void Delete(int id);
        Venue GetById(int id);
        List<Venue> GetAll();
    }
}
