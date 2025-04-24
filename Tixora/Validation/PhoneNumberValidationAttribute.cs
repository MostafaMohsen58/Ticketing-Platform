using System.ComponentModel.DataAnnotations;

namespace Tixora.Validation
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string phoneNumber)
            {
                // Check if exactly 11 digits AND starts with "01"
                return !string.IsNullOrEmpty(phoneNumber) &&
                       System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^01\d{9}$");
            }
            return false;
        }

        public PhoneNumberValidationAttribute()
        {
            ErrorMessage = "Phone number must be exactly 11 digits and start with '01'";
        }
    }
}
