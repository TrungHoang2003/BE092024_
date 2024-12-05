using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Common.DataValidation;

public class StringValidate: ValidationAttribute
{
    private static readonly Regex _htmlTagRegex = new Regex("<.*?>", RegexOptions.Compiled);
    private static readonly Regex _specialCharRegex = new Regex("[^a-zA-Z0-9\\s]", RegexOptions.Compiled); // Kiểm tra ký tự đặc biệt ngoài chữ cái và số

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(ErrorMessage?? "Value cannot be null");
        }
        
        string? stringValue = value as string;
        {
            if (stringValue == null || string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult(ErrorMessage ?? "Value cannot be empty");
            }
        }

        if (_specialCharRegex.IsMatch(stringValue))
        {
            return new ValidationResult(ErrorMessage ??"String contains special characters.");
        }

        if (_htmlTagRegex.IsMatch(stringValue))
        {
            return new ValidationResult(ErrorMessage ??"String contains HTML tags.");
        }
        
        return ValidationResult.Success;
    }
}