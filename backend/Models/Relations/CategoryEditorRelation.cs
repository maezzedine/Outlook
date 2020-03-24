using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Relations
{
    public class CategoryEditorRelation
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
    }
}
