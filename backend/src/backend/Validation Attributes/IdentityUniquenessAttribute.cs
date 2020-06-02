using backend.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace backend.Validation_Attributes
{
    public class IdentityUniquenessAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (OutlookContext)validationContext.GetService(typeof(OutlookContext));
            var existingUserWithSameUsername = context.Users.SingleOrDefault(u => u.UserName == value.ToString());

            if (existingUserWithSameUsername != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string v) => $"Username {v} is already taken.";
    }
}
