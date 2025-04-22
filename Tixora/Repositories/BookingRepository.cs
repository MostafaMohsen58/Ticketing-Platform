using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TixoraContext context;
        public BookingRepository(TixoraContext _context )
        {
            context = _context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return context.Bookings
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .ToList();
        }

        public Booking GetById(int id)
        {
            var booking = context.Bookings
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {id} not found.");
            }
            return booking;
        }
        public void Add(Booking obj)
        {
            context.Bookings.Add(obj);
        }
        public void Update(Booking updatedBooking)
        {
            context.Bookings
                .Where(b => b.Id == updatedBooking.Id)
                .ExecuteUpdate(setters => setters
                .SetProperty(b => b.Amount, updatedBooking.Amount)
                .SetProperty(b => b.TransactionId, updatedBooking.TransactionId)
                //.SetProperty(b => b.BookedAt, updatedBooking.BookedAt)
                .SetProperty(b => b.TicketId, updatedBooking.TicketId)
                //.SetProperty(b => b.UserId, updatedBooking.UserId);
                );
        }

        public void Delete(int id)
        {
            var booking = context.Bookings.Find(id);
            
            context.Bookings.Remove(booking);
        }
        public IEnumerable<Booking> GetByUserId(string userId)
        {
            return context.Bookings
                .Where (b => b.UserId == userId)
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .ToList();
        }
        public IEnumerable<Booking> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return context.Bookings
                .Where(b => b.BookedAt >= startDate && b.BookedAt <= endDate)
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .ToList();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

    }
}
