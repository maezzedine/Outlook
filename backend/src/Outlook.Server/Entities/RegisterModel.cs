using Outlook.Server.Validation_Attributes;
using System.ComponentModel.DataAnnotations;

namespace Outlook.Server.Entities
{
    public class RegisterModel
    {
        [Required]
        [IdentityUniqueness]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [EmailUniqueness]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
