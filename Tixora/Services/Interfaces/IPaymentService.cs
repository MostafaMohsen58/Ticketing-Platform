namespace Tixora.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateStripeSession(int bookingId);
    }
}
