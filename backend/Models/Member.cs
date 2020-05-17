using backend.Models.Interfaces;
using backend.Validation_Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Member : IMember
    {
        public int ID { get; set; }

        [MemberUniqueness]
        [RegularExpression(@"^[a-zA-Z\uFE70–\uFEFF.\-\s\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]*$", ErrorMessage = "Characters, numbers and special symbols are not allowed.")]
        public string Name { get; set; }

        public Position Position { get; set; }

        public List<Article> Articles { get; set; }

        public Category Category { get; set; }
    }
}
