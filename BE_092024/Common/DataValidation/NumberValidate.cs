using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Common.DataValidation;

public class NumberValidate: ValidationAttribute
{
   private readonly Regex _digitsRegex = new Regex("^[0-9]+$", RegexOptions.Compiled); // Kiểm tra chỉ số từ 0 đến 9
   protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
   {
      if (value == null)
      {
         return new ValidationResult("Value cannot be null.");
      }
     
      string? stringValue = value as string;

      if (stringValue == null || !_digitsRegex.IsMatch(stringValue))
      {
         return new ValidationResult("Value is not a number.");
      } 
      return ValidationResult.Success; 
   }
}