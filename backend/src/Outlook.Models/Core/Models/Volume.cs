using Outlook.Models.Attributes.Validation;
using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Outlook.Models.Core.Models
{
    public class Volume : IVolume<Issue>
    {
        public int Id { get; set; }

        [VolumeNumberUniqueness]
        public int Number { get; set; }

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
