using System.ComponentModel.DataAnnotations;

namespace Tixora.Models
{
    public class Venue
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Venue name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 1000000, ErrorMessage = "Capacity must be between 1 and 1,000,000")]
        public int Capacity { get; set; }

        public virtual List<Event>? Events { get; set; }
    }
}
