using System.ComponentModel.DataAnnotations;
using Tixora.Models;
using Tixora.Validation;

namespace Tixora.ViewModels
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; } // Or your Gender enum

        [Url(ErrorMessage = "Invalid URL format")]
        [Display(Name = "Profile Picture")]
        public string? ProfileUrl { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Phone] // Basic phone number validation
        [Display(Name = "Phone Number")]
        [PhoneNumberValidation(ErrorMessage = "Phone number must start with '01' and have 11 digits.")]
        public string PhoneNumber { get; set; }
    }
}
