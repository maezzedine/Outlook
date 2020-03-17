using backend.Models;
using System.Collections.Generic;

namespace backend.Areas.Identity
{
    public interface IUser
    {
        public string Name { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfReactions { get; set; }
    }
    
}