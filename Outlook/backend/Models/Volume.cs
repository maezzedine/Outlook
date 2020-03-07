using backend.Models.Interfaces;
using System.Collections.Generic;

namespace backend.Models
{
    public class Volume : IVolume
    {
        public int Id { get; set; }
        public int VolumeNumber { get; set; }
        public int FallYear { get; set; }
        public int SpringYear { get; set; }
        public ICollection<IMember> ArabicBoard { get; set; }
        public ICollection<IMember> EnglishBoard { get; set; }
    }
}
