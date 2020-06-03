using System;

namespace Outlook.Models.Core.Interfaces
{
    public interface IRatedBlog
    {
        public int Rate { get; set; }

        public int NumberOfVotes { get; set; }

        public int NumberOfFavorites { get; set; }

        public DateTime DateTime { get; set; }
    }
}
