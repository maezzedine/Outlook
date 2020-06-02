using Outlook.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Outlook.Models.Attributes.Validation
{
    class CategoryNameUniqueness : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Category name is required.");
            }

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
