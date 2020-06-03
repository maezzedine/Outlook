using Outlook.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Outlook.Models.Attributes.Validation
{
    public class MemberNameUniqueness : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            var context = (OutlookContext)validationContext.GetService(typeof(OutlookContext));
            var existingMemberWithSameName = context.Member.SingleOrDefault(m => m.Name == value.ToString());

            if (existingMemberWithSameName != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage(string v) => $"Member with name {v} already exists.";
    }
}
