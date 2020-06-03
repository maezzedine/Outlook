using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;

namespace Outlook.Models.Core.Dtos
{
    public class VolumeSummaryDto : IVolumeSummary
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int FallYear { get; set; }

        public int SpringYear { get; set; }
    }

    public class VolumeDto : VolumeSummaryDto, IVolume<IssueDto>
    {
        public List<IssueDto> Issues { get; set; }
    }
}
