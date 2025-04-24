using Tixora.Models;
using Tixora.ViewModels;

namespace Tixora.Services.Interfaces
{
    public interface IBookingService
    {
        /// Repository methods
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
        Task<int> SaveAsync();

        /// Custom methods
        Task<Booking> CreateBookingAsync(CreateBookingViewModel viewModel, string userId);
        Task ConfirmBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
    }
}
