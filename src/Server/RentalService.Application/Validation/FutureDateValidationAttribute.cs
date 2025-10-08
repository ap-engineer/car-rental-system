using System.ComponentModel.DataAnnotations;

namespace RentalService.Application.Validation;

public class FutureDateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime <= DateTime.Now;
        }
        return true; // Let Required attribute handle null values
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} cannot be in the future";
    }
}
