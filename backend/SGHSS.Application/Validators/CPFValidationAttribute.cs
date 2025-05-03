using System.ComponentModel.DataAnnotations;

namespace Application.Validators;

/// <summary>
/// Custom validation attribute for CPF (Brazilian tax ID)
/// </summary>
public class CPFValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Validates if the CPF is valid according to Brazilian rules
    /// </summary>
    /// <param name="value">The CPF value to validate</param>
    /// <param name="validationContext">The validation context</param>
    /// <returns>ValidationResult.Success if valid, otherwise a validation error</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        var cpf = value.ToString().Trim().Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return new ValidationResult("CPF must have 11 digits");

        if (!cpf.All(char.IsDigit))
            return new ValidationResult("CPF must contain only digits");

        // Check if all digits are the same (invalid CPF)
        if (cpf.Distinct().Count() == 1)
            return new ValidationResult("Invalid CPF");

        // Calculate first digit
        var sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (cpf[i] - '0') * (10 - i);

        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;

        if (digit1 != (cpf[9] - '0'))
            return new ValidationResult("Invalid CPF");

        // Calculate second digit
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (cpf[i] - '0') * (11 - i);

        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;

        if (digit2 != (cpf[10] - '0'))
            return new ValidationResult("Invalid CPF");

        return ValidationResult.Success;
    }
} 