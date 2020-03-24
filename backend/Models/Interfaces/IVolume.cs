using System.Collections;
using System.Collections.Generic;

namespace backend.Models.Interfaces
{
    public interface IVolume
    {
        public int Id { get; set; }
        public int VolumeNumber { get; set; }
        public int FallYear { get; set; }
        public int SpringYear { get; set; }
    }
}