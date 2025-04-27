using Tixora.Models;
using Tixora.ViewModels;
using Tixora.ViewModels.BookingViewModel;

namespace Tixora.Services.Interfaces
{
    public interface IBookingService
    {
        /// Repository methods
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        Task<bool> UpdateAsync(EditBookingViewModel viewModel, int bookingId);
        Task DeleteAsync(int id);
        Task<int> SaveAsync();

        /// Custom methods
        Task<Booking> CreateBookingAsync(CreateBookingViewModel viewModel, string userId);
        Task ConfirmBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
    }
}
