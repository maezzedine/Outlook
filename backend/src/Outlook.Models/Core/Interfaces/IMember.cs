using System.Collections.Generic;

namespace Outlook.Models.Core.Interfaces
{
    public interface IMemberSummary
    {
        public string Name { get; set; }
    }

    public interface IMember<T> : IMemberSummary where T : IArticleSummary
    {
        public List<T> Articles { get; set; }
    }

    public interface IMember : IMemberSummary
    {
        public List<IArticleSummary> Articles { get; set; }
    }
}
