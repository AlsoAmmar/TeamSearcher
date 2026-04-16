using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TeamSearcher.Models;

public class PhoneNumberAttribute : ValidationAttribute
{
    private static readonly Regex EgyptRegex = new(
        @"^01[0125][0-9]{8}$",
        RegexOptions.Compiled
    );

    private static readonly Regex InternationalRegex = new(
        @"^\+[1-9][0-9]{6,13}$",
        RegexOptions.Compiled
    );

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;
        
        var number = value.ToString()!;
        var normalized = Normalize(number);

        if (IsEgyptNumber(normalized) || IsInternationalNumber(normalized))
        {
            return ValidationResult.Success;
        }
        
        return new ValidationResult(ErrorMessage ?? "رقم الهاتف غير صحيح");
    }
    
    private static string Normalize(string phone) =>
        phone.Trim().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
    
    private static bool IsEgyptNumber(string phone) =>
        EgyptRegex.IsMatch(phone);

    private static bool IsInternationalNumber(string phone) =>
        phone.StartsWith('+') && InternationalRegex.IsMatch(phone);
}