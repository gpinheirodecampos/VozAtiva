using System.ComponentModel.DataAnnotations;

namespace RentAPI.Validations
{
    public class EmailValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value.ToString().Contains('@') && value.ToString().Contains(".com"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Formato incorreto.");
        }
    }
}
