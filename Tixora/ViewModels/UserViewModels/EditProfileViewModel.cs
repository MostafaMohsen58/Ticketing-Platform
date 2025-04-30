using System.ComponentModel.DataAnnotations;
using Tixora.Models;
using Tixora.Validation;

namespace Tixora.ViewModels.UserViewModels
{
    public class EditProfileViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        //[Url(ErrorMessage = "Invalid URL format")]
        [Display(Name = "Profile Picture")]
        public string? ProfileUrl { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        [PhoneNumberValidation(ErrorMessage = "Phone number must start with '01' and have 11 digits.")]
        public string PhoneNumber { get; set; }
        
       
    }
}
