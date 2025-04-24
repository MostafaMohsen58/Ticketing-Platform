using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;

namespace Tixora.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TixoraContext context;
        public BookingRepository(TixoraContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await context.Bookings
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await context.Bookings
                    .Include(b => b.User)
                    .Include(b => b.Ticket)
                        .ThenInclude(t => t.Event)
                    .FirstOrDefaultAsync(b => b.Id == id)
                     ?? throw new KeyNotFoundException($"Booking {id} not found");
        }
        public async Task AddAsync(Booking booking)
        {
            var ticket = await context.Tickets.FindAsync(booking.TicketId);
            if (ticket.AvailableQuantity < booking.Amount)
            {
                throw new InvalidOperationException("Not enough tickets available");
            }
            await context.Bookings.AddAsync(booking);
            ticket.AvailableQuantity -= booking.Amount; // Decrease the available quantity
        }
        public async Task UpdateAsync(Booking updatedBooking)
        {
            var existingBooking = await context.Bookings.FindAsync(updatedBooking.Id);
            if (existingBooking.TicketId != updatedBooking.TicketId ||
                existingBooking.Amount != updatedBooking.Amount)
            {
                var oldTicket = await context.Tickets.FindAsync(existingBooking.TicketId);
                var newTicket = await context.Tickets.FindAsync(updatedBooking.TicketId);
                if (newTicket.AvailableQuantity < updatedBooking.Amount)
                {
                    throw new InvalidOperationException("Not enough tickets available");
                }
                oldTicket.AvailableQuantity += existingBooking.Amount; // Return quantity to old ticket
                newTicket.AvailableQuantity -= updatedBooking.Amount;
            }
            await context.Bookings
            .Where(b => b.Id == updatedBooking.Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(b => b.Amount, updatedBooking.Amount)
            .SetProperty(b => b.TransactionId, updatedBooking.TransactionId)
            .SetProperty(b => b.TicketId, updatedBooking.TicketId)
            );
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await context.Bookings.FindAsync(id);

            var ticket = await context.Tickets.FindAsync(booking.TicketId); // Return tickets to available quantity
            context.Bookings.Remove(booking);
        }
        public async Task<IEnumerable<Booking>> GetByUserIdAsync(string userId)
        {
            return await context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await context.Bookings
                .Where(b => b.BookedAt >= startDate && b.BookedAt <= endDate)
                .Include(b => b.User)
                .Include(b => b.Ticket)
                .ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

    }
}
