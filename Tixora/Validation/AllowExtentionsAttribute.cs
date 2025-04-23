using System.ComponentModel.DataAnnotations;

namespace Tixora.Validation
{
    public class AllowExtentionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtention;
        public AllowExtentionsAttribute(string allowExtentions)
        {
            _allowedExtention = allowExtentions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var IsValidExtention = _allowedExtention.Split(',').Contains(extention, StringComparer.OrdinalIgnoreCase);
                if (!IsValidExtention)
                {
                    return new ValidationResult($"only {_allowedExtention} are allowed ");
                }
            }
            return ValidationResult.Success!;
        }
    }
    
}
           
        
    

