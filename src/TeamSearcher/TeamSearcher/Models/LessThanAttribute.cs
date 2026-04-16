using System.ComponentModel.DataAnnotations;

namespace TeamSearcher.Models;

public class LessThanAttribute : ValidationAttribute
{
    private readonly int _max;

    public LessThanAttribute(int number)
    {
        _max = number;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var number = int.Parse(value!.ToString()!);
        
        if (number < _max)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}