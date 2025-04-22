using System.ComponentModel.DataAnnotations;
using Tixora.Validation;

namespace Tixora.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        [PhoneNumberValidation(ErrorMessage = "phone number must be starts with \"01\" in addition to having 11 digits. ")]
        public string ContactPhone { get; set; }
        public string? LogoUrl { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}
