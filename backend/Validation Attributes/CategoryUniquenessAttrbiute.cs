using backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Validation_Attributes
{
    public class CategoryUniquenessAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (OutlookContext)validationContext.GetService(typeof(OutlookContext));
            var existingCategoryWithSameName = context.Category.SingleOrDefault(c => c.CategoryName == value.ToString());

            if (existingCategoryWithSameName != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string v) => $"Category with name {v} already exists.";
    }
}
