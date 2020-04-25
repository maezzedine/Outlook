using backend.Areas.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Relations
{
    public class UserRateArticle
    {
        public int ID { get; set; }
        
        public OutlookUser User { get; set; }
        
        public Article Article { get; set; }
        
        public UserRate Rate { get; set; }
        
        public enum UserRate
        {
            None, Up, Down
        }
    }
}
