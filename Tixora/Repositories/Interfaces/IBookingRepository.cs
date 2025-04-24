using System.Collections;
using Tixora.Models;

namespace Tixora.Repositories.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking>  GetAll();
        Booking GetById(int id);
        void Delete(int id);
        IEnumerable<Booking> GetByUserId(string userId);
        IEnumerable<Booking> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}
