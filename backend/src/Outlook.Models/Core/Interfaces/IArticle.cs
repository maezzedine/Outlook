using System.Collections.Generic;

namespace Outlook.Models.Core.Interfaces
{
    public interface IArticleSummary
    {
        public string Language { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string PicturePath { get; set; }

        public string Text { get; set; }
    }

    public interface IArticle<T, U, V, W> : IArticleSummary where T : IMemberSummary
                                                            where U : IIssueSummary
                                                            where V : ICategorySummary
                                                            where W : IComment
    {
        public T Writer { get; set; }

        public U Issue { get; set; }

        public V Category { get; set; }

        public List<W> Comments { get; set; }
    }

    public interface IArticle : IArticleSummary
    {
        public IMember<IArticleSummary> Writer { get; set; }

        public IIssue<IArticleSummary> Issue { get; set; }

        public ICategory<IArticleSummary, IMemberSummary> Category { get; set; }

        public List<IComment> Comments { get; set; }
    }
}
