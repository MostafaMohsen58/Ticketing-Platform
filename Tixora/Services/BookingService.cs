using Tixora.Models;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;
using Tixora.ViewModels.BookingViewModel;

namespace Tixora.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly IEventsService eventsService;
        public BookingService(IBookingRepository _bookingRepository,
                                ITicketRepository _ticketRepository,
                                IEventsService _eventsService)
        {
            bookingRepository = _bookingRepository;
            ticketRepository = _ticketRepository;
            eventsService = _eventsService;
        }
        
        public async Task<Booking> CreateBookingAsync(CreateBookingViewModel viewModel, string userId)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel.Amount <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(viewModel.Amount));

            var ticket =await ticketRepository.GetById(viewModel.TicketId);
            var booking = new Booking
            {
                UserId = userId,
                TicketId = viewModel.TicketId,
                Amount = viewModel.Amount,
                BookedAt = DateTime.UtcNow,
                TransactionId = 0, // Placeholder

            };
            await ConfirmBookingAsync(booking);
            ticket.AvailableQuantity -= viewModel.Amount; // Decrease the available quantity
            await ticketRepository.UpdateAsync(ticket); // Update the ticket's available quantity

            //await SendBookingConfirmation(booking, booking.User.Id);
            return booking;
        }
        //private async Task SendBookingConfirmation(Booking booking, string userId)
        //{
        //    // Logic to send booking confirmation (email)
        //    // This is a placeholder for the actual implementation
        //    await Task.CompletedTask;
        //}
        public async Task ConfirmBookingAsync(Booking booking)
        {
           await bookingRepository.AddAsync(booking);
           await bookingRepository.SaveAsync(); // Save the booking to the database

        }
        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            var booking = await bookingRepository.GetByDateRangeAsync(startDate: DateTime.UtcNow.AddDays(-30), endDate: DateTime.UtcNow);
            return booking.Where(b => b.Amount > 0);
        }
        public async Task<bool> UpdateAsync(EditBookingViewModel viewModel, int bookingId)
        {
            var booking = await bookingRepository.GetByIdAsync(bookingId);
            var newticket =await ticketRepository.GetById(viewModel.TicketId);
            if (booking.TicketId != viewModel.TicketId)
            {
                var oldTicket =await ticketRepository.GetById(booking.TicketId);
                oldTicket.AvailableQuantity += booking.Amount; // Increase the available quantity of the old ticket
                await ticketRepository.UpdateAsync(oldTicket); // Update the old ticket's available quantity
            }
            newticket.AvailableQuantity -= viewModel.CurrentQuantity; // Decrease the available quantity of the new ticket
            await ticketRepository.UpdateAsync(newticket); // Update the new ticket's available quantity

            booking.BookedAt = DateTime.UtcNow;
            booking.TicketId = viewModel.TicketId;
            booking.Amount = viewModel.CurrentQuantity;
            await bookingRepository.UpdateAsync(booking);
            await bookingRepository.SaveAsync(); // Save the changes to the database

            return true;
        }

        // Repository methods
        public async Task<IEnumerable<Booking>> GetAllAsync() => await bookingRepository.GetAllAsync();
        public async Task<Booking> GetByIdAsync(int id) => await bookingRepository.GetByIdAsync(id);
        public async Task AddAsync(Booking booking) => await bookingRepository.AddAsync(booking);
        public async Task DeleteAsync(int id) => await bookingRepository.DeleteAsync(id);
        public async Task<int> SaveAsync() => await bookingRepository.SaveAsync();

        
    }
}
