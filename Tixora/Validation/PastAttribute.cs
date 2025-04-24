using System.ComponentModel.DataAnnotations;

namespace Tixora.Validation
{
    public class PastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly dateValue)
            {
                if (dateValue > DateOnly.FromDateTime(DateTime.Today))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success!;
        }
    }
}
