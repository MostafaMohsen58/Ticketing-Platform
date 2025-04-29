using Tixora.Models;

namespace Tixora.ViewModels
{
    public class TicketIndexViewModel
    {
        public float? Price { get; set; }
        public TicketStatus? Status { get; set; }

        public IEnumerable<Ticket> Tickets { get; set; } = new List<Ticket>();


       
    }
}
