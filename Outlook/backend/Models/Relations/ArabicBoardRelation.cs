using backend.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Relations
{
    public class ArabicBoardRelation
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int VolumeID { get; set; }
        public Position Position { get; set; }
    }
}
