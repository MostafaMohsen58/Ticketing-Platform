using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;

namespace Tixora.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly IEventsService eventsService;
        public BookingService(IBookingRepository _bookingRepository, ITicketRepository _ticketRepository, IEventsService _eventsService)
        {
            bookingRepository = _bookingRepository;
            ticketRepository = _ticketRepository;
            eventsService = _eventsService;
        }
        private Booking MapToBooking(CreateBookingViewModel viewModel, string userId, Ticket ticket)
        {
            return new Booking
            {
                Amount = viewModel.Amount,
                TicketId = viewModel.TicketId,
                UserId = userId,
                BookedAt = DateTime.UtcNow,
                //TransactionId = GenerateTransactionId(),
                Ticket = ticket
            };
        }
        private string GenerateTransactionId()
        {
            return $"TXN-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 4)}";
        }
        public async Task<Booking> CreateBookingAsync(CreateBookingViewModel viewModel)
        {
            var ticket = ticketRepository.GetById(viewModel.TicketId);
            if (ticket.AvailableQuantity < viewModel.Amount)
                throw new InvalidOperationException($"Not enough tickets available. Only {ticket.AvailableQuantity} left.");
            var booking = MapToBooking(viewModel, userId: "userId", ticket);
            await ConfirmBookingAsync(booking);
            return booking;
        }
        public async Task ConfirmBookingAsync(Booking booking)
        {
            var existingBooking = await bookingRepository.GetByIdAsync(booking.Id);

            var ticket = (await bookingRepository.GetByIdAsync(booking.TicketId))?.Ticket;
            if (ticket.AvailableQuantity < booking.Amount)
            {
                throw new InvalidOperationException("Not enough tickets available.");
            }
            // Decrease the available quantity
            ticket.AvailableQuantity -= booking.Amount;
            await ticketRepository.UpdateAsync(ticket); // Update the ticket's available quantity

            bookingRepository.AddAsync(booking);
            bookingRepository.SaveAsync(); // Save the booking to the database

        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            var booking = await bookingRepository.GetByDateRangeAsync(startDate: DateTime.UtcNow.AddDays(-30), endDate: DateTime.UtcNow);
            return booking.Where(b => b.Amount > 0);
        }

        // Repository methods
        public async Task<IEnumerable<Booking>> GetAllAsync() => await bookingRepository.GetAllAsync();
        public async Task<Booking> GetByIdAsync(int id) => await bookingRepository.GetByIdAsync(id);
        public async Task AddAsync(Booking booking) => await bookingRepository.AddAsync(booking);
        public async Task UpdateAsync(Booking booking) => await bookingRepository.UpdateAsync(booking);
        public async Task DeleteAsync(int id) => await bookingRepository.DeleteAsync(id);
        public async Task<int> SaveAsync() => await bookingRepository.SaveAsync();

    }
}
