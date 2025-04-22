using System.ComponentModel.DataAnnotations;

namespace Tixora.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [StringLength(200, ErrorMessage = "Image URL cannot exceed 200 characters.")]
        [Url(ErrorMessage = "Invalid Image URL format.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; }

        public virtual List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual Venue Venue { get; set; }
        [Required(ErrorMessage = "Venue ID is required.")]
        public int VenueId { get; set; }
        public virtual Organizer Organizer { get; set; }
        [Required(ErrorMessage = "Organizer ID is required.")]
        public int OrganizerId { get; set; }
    }
}
