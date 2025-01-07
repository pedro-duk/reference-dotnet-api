using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations;

// Validates if the first letter of a string is in uppercase
// To use, simply import APICatalogo.Validations and use the [PrimeiraLetraMaiuscula] annotation
public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        } // bypasses other validations to avoid errors

        var primeiraLetra = value.ToString()[0].ToString();
        if (primeiraLetra != primeiraLetra.ToUpper())
        {
            return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula");
        }

        return ValidationResult.Success;
    }
}
