using Outlook.Models.Attributes.Validation;
using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;

namespace Outlook.Models.Core.Models
{
    public class Category : ICategory<Article, Member>
    {
        public int Id { get; set; }

        public string Language { get; set; }

        [CategoryNameUniqueness]
        public string Name { get; set; }

        public string Tag { get; set; }

        public List<Member> Editors { get; set; }

        public List<Article> Articles { get; set; }

        public Category SetLanguage(string language)
        {
            Language = language;
            return this;
        }

        public Category SetTag(string tag)
        {
            Tag = tag;
            return this;
        }
    }
}
