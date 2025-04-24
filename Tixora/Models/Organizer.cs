
using System.ComponentModel.DataAnnotations;
using Tixora.Validation;

namespace Tixora.Models
{
    public class Organizer
    {
        public Organizer()
        {
            Events = new List<Event>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [PhoneNumberValidation(ErrorMessage = "Phone number must start with \"01\" and be 11 digits long")]
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }

        [Display(Name = "Logo URL")]
        public string? LogoUrl { get; set; }

        public virtual List<Event> Events { get; set; }
    }
}