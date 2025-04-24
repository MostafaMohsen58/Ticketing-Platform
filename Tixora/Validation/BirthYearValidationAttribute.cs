using System.ComponentModel.DataAnnotations;

namespace Tixora.Validation
{
    public class BirthYearValidationAttribute : ValidationAttribute
    {
        private readonly int _minimumYear;

        public BirthYearValidationAttribute(int minimumYear = 2007)
        {
            _minimumYear = minimumYear;
            ErrorMessage = $"Birth year must be after {_minimumYear}";
        }

        public override bool IsValid(object? value)
        {
            if (value is DateOnly dateOfBirth)
            {
                return dateOfBirth.Year <= _minimumYear;
            }
            return false;
        }
    }
}
