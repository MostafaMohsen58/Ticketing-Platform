using System.ComponentModel.DataAnnotations;

namespace Tixora.ViewModels.UserViewModels
{
    public class AccountInformation
    {
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}
