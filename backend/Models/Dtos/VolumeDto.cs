using System.Collections.Generic;

namespace backend.Models.Dtos
{
    public class VolumeSummaryDto
    {
        public int Id { get; set; }

        public int VolumeNumber { get; set; }

        public int FallYear { get; set; }

        public int SpringYear { get; set; }
    }

    public class VolumeDto : VolumeSummaryDto
    {
        public List<IssueDto> Issues { get; set; }
    }
}
