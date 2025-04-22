using System.ComponentModel.DataAnnotations;
using Tixora.Models;
using Tixora.Validation;

namespace Tixora.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm password must match password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select a gender")]
        public Gender Gender { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [Display(Name = "Profile Picture")]
        public string? ProfileUrl { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [PhoneNumberValidation(ErrorMessage = "phone number must be starts with \"01\" in addition to having 11 digits. ")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } // Note: Consider changing this to string since it's also in IdentityUser

        [Required(ErrorMessage = "National ID is required")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be exactly 14 digits")]
        [Display(Name = "National ID")]
        public string NationalId { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Past(ErrorMessage = "Date of birth must be in the past")]
        [BirthYearValidation(ErrorMessage = "Birth year must be before 2008")]
        public DateOnly DateOfBrith { get; set; } // Note: Typo in property name (Birth is misspelled)

    }
}
