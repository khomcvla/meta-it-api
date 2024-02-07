using System.ComponentModel.DataAnnotations;

namespace MetaITAPI.Utils.Attributes;

public class NullEmptyOrWhiteSpaceAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
        {
            return new ValidationResult(ErrorMessage ?? "The field must not consist only of whitespace.");
        }

        return ValidationResult.Success;
    }
}
