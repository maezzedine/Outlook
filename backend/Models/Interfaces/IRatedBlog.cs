using System;

namespace backend.Models.Interfaces
{
    interface IRatedBlog
    {
        public int Rate { get; set; }
        
        public int NumberOfVotes { get; set; }
        
        public int NumberOfFavorites { get; set; }
        
        public DateTime DateTime { get; set; }

        public void RateUp();
        
        public void UnRateUp();
        
        public void RateDown();
        
        public void UnRateDown();
    }
}
