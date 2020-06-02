using System.Collections.Generic;

namespace Outlook.Models.Core.Interfaces
{
    public interface IVolumeSummary
    {
        public int Number { get; set; }

        public int FallYear { get; set; }

        public int SpringYear { get; set; }
    }

    public interface IVolume<T> : IVolumeSummary where T : IIssueSummary
    {
        public List<T> Issues { get; set; }
    }

    public interface IVolume : IVolumeSummary
    {
        public List<IIssueSummary> Issues { get; set; }
    }
}
