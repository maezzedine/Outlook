using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace backend.Services
{
    public class MemberService
    {
        public static Position[] ArabicPositions = { Position.المحرر, Position.رئيس_تحرير, Position.رئيس_قسم, Position.عضو_سابق, Position.كاتب_صحفي, Position.مدقق_الموقع, Position.مدقق_النسخة, Position.مدقق_لغوي, Position.نائب_المحرر };
        public static Position[] EnglishPositions = { Position.Editor_In_Chief, Position.Senior_Editor, Position.Associate_Editor, Position.Junior_Editor, Position.Proofreader, Position.Copy_Editor, Position.Web_Editor, Position.Former_Member, Position.Staff_Writer };

        public static Position[] NonBoardMembers = { Position.Staff_Writer, Position.Former_Member, Position.كاتب_صحفي, Position.عضو_سابق };

        public static bool IsJuniorEditor(Member member) => (member.Position == Position.Junior_Editor) || (member.Position == Position.رئيس_قسم);

        public static void GetMemberLanguageAndArticlesCount(Member member, OutlookContext context)
        {
            if (ArabicPositions.Contains(member.Position))
            {
                member.Language = Language.Arabic;
            }
            else if (EnglishPositions.Contains(member.Position))
            {
                member.Language = Language.English;
            }

            member.NumberOfArticles = context.Article.Where(a => a.MemberID == member.ID).Count();
        }

        public static void GetJuniorEditorCategory(Member member, OutlookContext context)
        {
            if (IsJuniorEditor(member))
            {
                var categoryId = context.CategoryEditor.FirstOrDefault(c => c.MemberID == member.ID).CategoryID;
                var category = context.Category.Find(categoryId);
                member.Category = category;
            }
        }

        public static void AddBoardMembers(Dictionary<string, IQueryable<Member>> section, IOrderedEnumerable<Position> positions, IQueryable<Member> boardMembers)
        {
            foreach (var position in positions)
            {
                if (!NonBoardMembers.Contains(position))
                {
                    var members = from member in boardMembers
                                  where member.Position == position
                                  select member;

                    section[position.ToString().Replace('_', ' ')] = members;
                }
            }
        }

    }
}
