using backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Validation_Attributes
{
    public class MemberUniquenessAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
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
