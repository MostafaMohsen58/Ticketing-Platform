
using Tixora.Models;

namespace Tixora.ViewModels.EventViewModel
{
    public class EntertainmentViewModel
    {
        public List<Event> Events { get; set; }
        public List<Venue> Venues { get; set; }
        public int? SelectedVenueId { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string SearchTerm { get; set; }
    }
}