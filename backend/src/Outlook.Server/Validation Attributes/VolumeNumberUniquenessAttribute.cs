using Outlook.Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Outlook.Server.Validation_Attributes
{
    public class VolumeNumberUniquenessAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (OutlookContext)validationContext.GetService(typeof(OutlookContext));
            var existingVolumeWithSameName = context.Volume.SingleOrDefault(v => v.VolumeNumber.ToString() == value.ToString());

            if (existingVolumeWithSameName != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage(string v) => $"Volume Number {v} already exists.";
    }
}
