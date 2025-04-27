using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IVenueRepository : IRepository<Venue>
    {
        void Delete(Venue venue);
        Task<Venue> GetById(int id);
        Task<List<Venue>> GetAll();
    }
}
