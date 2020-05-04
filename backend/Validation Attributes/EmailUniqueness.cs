using backend.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace backend.Validation_Attributes
{
    public class EmailUniqueness : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (OutlookContext)validationContext.GetService(typeof(OutlookContext));
            var emailAlreadyExists = context.Users.SingleOrDefault(u => u.Email == value.ToString());

            if (emailAlreadyExists != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(string value) => $"Email {value} is already taken.";
    }
}
