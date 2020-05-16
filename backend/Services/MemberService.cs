using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace backend.Services
{
    public class MemberService
    {
        private readonly OutlookContext context;

        public MemberService(OutlookContext context)
        {
            this.context = context;
        }

        public static Position[] ArabicPositions = { Position.المحرر, Position.رئيس_تحرير, Position.رئيس_قسم, Position.عضو_سابق, Position.كاتب_صحفي, Position.مدقق_الموقع, Position.مدقق_النسخة, Position.مدقق_لغوي, Position.نائب_المحرر };
        public static Position[] EnglishPositions = { Position.Editor_In_Chief, Position.Senior_Editor, Position.Associate_Editor, Position.Junior_Editor, Position.Proofreader, Position.Copy_Editor, Position.Web_Editor, Position.Former_Member, Position.Staff_Writer };
        public static Position[] NonBoardMembers = { Position.Staff_Writer, Position.Former_Member, Position.كاتب_صحفي, Position.عضو_سابق };

        /// <summary>
        /// IsJuniorEditor is a method that returns whether a member is an arabic or english junior editor
        /// </summary>
        /// <param name="member"></param>
        /// <returns>boolean result</returns>
        public bool IsJuniorEditor(Member member) => (member.Position == Position.Junior_Editor) || (member.Position == Position.رئيس_قسم);

        /// <summary>
        /// IsWriter is a method that returns whether a member is an arabic or english writer
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool IsWriter(Member member) => (member.Position == Position.Staff_Writer) || (member.Position == Position.كاتب_صحفي);

        /// <summary>
        /// GetMemberLanguageAndArticlesCount is a method that retrieves a member's language and number of articles
        /// </summary>
        /// <param name="member"></param>
        public void GetMemberLanguageAndArticlesCount(Member member)
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

        /// <summary>
        /// GetJuniorEditorCategory is a method that retrieves a junior editor's category
        /// </summary>
        /// <param name="member"></param>
        public void GetJuniorEditorCategory(Member member)
        {
            //if (IsJuniorEditor(member))
            //{
            //    var categoryEditor = context.CategoryEditor.FirstOrDefault(c => c.MemberID == member.ID);
            //    if (categoryEditor != null)
            //    {
            //        var category = context.Category.Find(categoryEditor.CategoryID);
            //        member.Category = category;
            //    }
            //}
        }

        /// <summary>
        /// AddBoardMembers is a method that retrieves all the members that belong to a section, given its positions, from a given set of members
        /// </summary>
        /// <param name="section"></param>
        /// <param name="positions">positions of the mentioned sections</param>
        /// <param name="boardMembers">set of members</param>
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
