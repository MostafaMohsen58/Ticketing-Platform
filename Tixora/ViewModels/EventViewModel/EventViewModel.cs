using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tixora.ViewModels.EventViewModel
{
    public class EventViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = default!;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; } = default!;
        [Display(Name = "Venue")]
        [Required(ErrorMessage = "Venue ID is required.")]
        public int VenueId { get; set; }
        public IEnumerable<SelectListItem> Venues = Enumerable.Empty<SelectListItem>();

        [Required(ErrorMessage = "Organizer ID is required.")]
        [Display(Name = "Organizer")]
        public int OrganizerId { get; set; }
        public IEnumerable<SelectListItem> Organizers = Enumerable.Empty<SelectListItem>();
    }
}
