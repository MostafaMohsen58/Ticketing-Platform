using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Tixora.Validation;

namespace Tixora.Models
{
    public class ApplicationUser : IdentityUser
    {

        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        public string FirstName { get; set; } = default!;


        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        public string LastName { get; set; } = default!;

        public Gender Gender { get; set; }

        public string? ProfileUrl { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; } = default!;

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        public string City { get; set; } = default!;

        [PhoneNumberValidation(ErrorMessage = "phone number must be starts with \"01\" in addition to having 11 digits. ")]
        [MaxLength(11, ErrorMessage = "Phone number must be 11 digits")]
        public string PhoneNumber { get; set; } = default!;

        public string NationalId { get; set; } = default!;


        [DataType(DataType.Date)]
        public DateOnly DateOfBrith { get; set; } 

        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
