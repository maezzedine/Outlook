using Outlook.Models.Attributes.Validation;
using Outlook.Models.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Outlook.Models.Core.Models
{
    public class Member : IMember<Article>
    {
        public int Id { get; set; }

        [MemberNameUniqueness]
        [RegularExpression(@"^[a-zA-Z\uFE70–\uFEFF.\-\s+\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]*$", ErrorMessage = "Characters, numbers and special symbols are not allowed.")]
        public string Name { get; set; }

        public string Position { get; set; }

        public List<Article> Articles { get; set; }

        public Category Category { get; set; }
    }
}
