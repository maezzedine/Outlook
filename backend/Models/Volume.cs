using backend.Models.Interfaces;
using backend.Validation_Attributes;
using System.ComponentModel;

namespace backend.Models
{
    public class Volume : IVolume
    {
        public int Id { get; set; }

        [DisplayName("Volume Number")]
        [VolumeNumberUniqueness]
        public int VolumeNumber { get; set; }

        [DisplayName("Fall Year")]
        public int FallYear { get; set; }

        [DisplayName("Spring Year")]
        public int SpringYear { get; set; }
    }
}
