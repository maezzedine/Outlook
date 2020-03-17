using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Relations
{
    public class UserRateArticle
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ArticleID { get; set; }
        public UserRate Rate { get; set; }
        public enum UserRate
        {
            None, Up, Down
        }
    }
}
