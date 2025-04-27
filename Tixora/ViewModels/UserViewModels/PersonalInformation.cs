using System.ComponentModel.DataAnnotations;
using Tixora.Validation;

namespace Tixora.ViewModels.UserViewModels
{
    public class PersonalInformation
    {
        public string? UserId { get; set; }
        [Display(Name = "Fan Name")]
        [Required(ErrorMessage = "Fan Name is required")]
        public string FanName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        [PhoneNumberValidation(ErrorMessage = "Phone number must start with '01' and have 11 digits.")]
        public string PhoneNumber { get; set; }
    }
}
