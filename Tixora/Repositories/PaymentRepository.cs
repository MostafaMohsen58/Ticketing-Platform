using Tixora.Models.Context;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;

namespace Tixora.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TixoraContext context;
        
        public PaymentRepository(TixoraContext _context)
        {
            context = _context;
        }
    }
}
