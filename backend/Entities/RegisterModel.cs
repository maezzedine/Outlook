using backend.Validation_Attributes;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    public class RegisterModel
    {
        [IdentityUniqueness]
        public string Username { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
