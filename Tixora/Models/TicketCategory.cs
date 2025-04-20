using System.ComponentModel.DataAnnotations;

namespace Tixora.Models
{
    public class TicketCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Range(0.1, 10.0, ErrorMessage = "Price multiplier must be between 0.1 and 10.0")]
        public float PriceMultiplier { get; set; }

        public virtual List<Ticket> Tickets { get; set; }
    }

}
