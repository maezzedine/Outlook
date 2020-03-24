using backend.Models;
using System.Collections.Generic;

namespace backend.Areas.Identity
{
    public interface IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
    }
    
}