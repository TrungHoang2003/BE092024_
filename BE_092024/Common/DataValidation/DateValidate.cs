using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Common.DataValidation;

public class DateValidate : ValidationAttribute
{
    private static readonly Regex _dateRegex = new Regex(@"^\d{4}-\d{2}-\d{2}$", RegexOptions.Compiled);

    override protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Value cannot be null.");
        }

        string? dateString = value as string;

        if (dateString == null || string.IsNullOrWhiteSpace(dateString))
        {
            return new ValidationResult(ErrorMessage ?? "Value cannot be empty");
        }
        
        if (!DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
        {
            return new ValidationResult("Date must have format: yyyy-MM-dd");
        }

        return ValidationResult.Success;
    }
}