using Stripe.Checkout;
using Tixora.Models;
using Tixora.Repositories;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;
using Tixora.ViewModels.BookingViewModel;

namespace Tixora.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ITicketService ticketService;
        private readonly IEventsService eventsService;
        public BookingService(IBookingRepository _bookingRepository,
                                ITicketService _ticketService,
                                IEventsService _eventsService)
        {
            bookingRepository = _bookingRepository;
            ticketService = _ticketService;
            eventsService = _eventsService;
        }
        
        public async Task<Booking> CreateBookingAsync(CreateBookingViewModel viewModel, string userId)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel.Amount <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(viewModel.Amount));

            var ticket =await ticketService.GetById(viewModel.TicketId);
            var booking = new Booking
            {
                //UserId = userId,
                //TicketId = viewModel.TicketId,
                //Amount = viewModel.Amount,
                //BookedAt = DateTime.UtcNow,
                //TransactionId = 0, // Placeholder

            };
            await ConfirmBookingAsync(booking);
            return booking;
        }
        public async Task ConfirmBookingAsync(Booking booking)
        {
           await bookingRepository.AddAsync(booking);
           await bookingRepository.SaveAsync(); // Save the booking to the database

        }
        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            var booking = await bookingRepository.GetByDateRangeAsync(startDate: DateTime.UtcNow.AddDays(-30), endDate: DateTime.UtcNow);
            return booking.Where(b => b.TicketQuantity > 0);
        }
        ///======================================================================================
        public async Task<Booking> CreatePendingBooking(BookingRequest request)
        {
            var ticketPrice = await ticketService.GetTicketPriceAsync(request.TicketId);
            var totalAmount = ticketPrice * request.Quantity;
            var booking = new Booking
            {
                TicketQuantity = request.Quantity,
                TotalAmount = totalAmount,
                UserId = request.UserId,
                TicketId = request.TicketId,
                PaymentStatus = "Pending",
                StripeSessionId = null,
                BookedAt = DateTime.UtcNow,
            };
            await bookingRepository.AddAsync(booking);
            await bookingRepository.SaveAsync();
            return booking;
        }
        public async Task<bool> UpdateBeforePay(Booking booking)
        {
            var bookingfromDb = await bookingRepository.GetByIdAsync(booking.Id);

            bookingfromDb.StripeSessionId = booking.StripeSessionId;

            await bookingRepository.UpdateAsync(bookingfromDb);
            await bookingRepository.SaveAsync(); // Save the changes to the database
            return true;
        }

        public async Task<bool> UpdateAfterPay(Booking booking)
        {
            var service = new SessionService();
            var session = await service.GetAsync(booking.StripeSessionId);
            if (session.PaymentStatus != "paid") return false;

            var ticket = await ticketService.GetById(booking.TicketId);

            ticket.AvailableQuantity -= booking.TicketQuantity; // Decrease the available quantity

            var bookingFromDb = await bookingRepository.GetByIdAsync(booking.Id);
            if (bookingFromDb == null) return false;

            bookingFromDb.PaymentStatus = "Paid";
            bookingFromDb.PaymentDate = DateTime.UtcNow;
            bookingFromDb.StripeSessionId = booking.StripeSessionId;

            try
            {
                await ticketService.Update(ticket);
                await bookingRepository.UpdateAsync(bookingFromDb);
                await bookingRepository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Repository methods
        public async Task<IEnumerable<Booking>> GetAllAsync() => await bookingRepository.GetAllAsync();
        public async Task<Booking> GetByIdAsync(int id) => await bookingRepository.GetByIdAsync(id);
        public async Task<Booking> GetByStripeSessionIdAsync(string stripeSessionId) => await bookingRepository.GetByStripeSessionIdAsync(stripeSessionId);
        public async Task AddAsync(Booking booking) => await bookingRepository.AddAsync(booking);
        public async Task UpdateAsync(Booking booking) => await bookingRepository.UpdateAsync(booking);
        public async Task DeleteAsync(int id) => await bookingRepository.DeleteAsync(id);
        public async Task<int> SaveAsync() => await bookingRepository.SaveAsync();

        // my ticket
        public async Task<IEnumerable<Booking>> GetbyUserId(string userId)
        {
            return await bookingRepository.GetByUserIdAsync(userId);
        }
    }
}
