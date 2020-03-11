using backend.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace backend.Models
{
    public class Volume : IVolume
    {
        public int Id { get; set; }
        [DisplayName("Volume Number")]
        public int VolumeNumber { get; set; }
        [DisplayName("Fall Year")]
        public int FallYear { get; set; }
        [DisplayName("Spring Year")]
        public int SpringYear { get; set; }
    }
}
