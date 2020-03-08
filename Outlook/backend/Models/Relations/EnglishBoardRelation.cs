using backend.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Relations
{
    public class EnglishBoardRelation
    {
        public int ID { get; set; }
        public Member Member { get; set; }
        public int VolumeID { get; set; }
        public Position Position { get; set; }
    }
}
