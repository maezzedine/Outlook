using Outlook.Server.Models.Interfaces;
using Outlook.Server.Validation_Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Outlook.Server.Models
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

        public List<Issue> Issues { get; set; }

        public Volume SetFallYear(int year)
        {
            FallYear = year;
            return this;
        }

        public Volume SetSpringYear(int year)
        {
            SpringYear = year;
            return this;
        }
    }
}
