using System.Collections.Generic;

namespace Outlook.Models.Core.Interfaces
{
    public interface ICategorySummary
    {
        public string Language { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }
    }

    public interface ICategory<T, U> : ICategorySummary where T : IArticleSummary
                                                        where U : IMemberSummary
    {
        public List<T> Articles { get; set; }

        public List<U> Editors { get; set; }
    }

    public interface ICategory : ICategorySummary
    {
        public List<IArticleSummary> Articles { get; set; }

        public List<IMemberSummary> Editors { get; set; }
    }
}
