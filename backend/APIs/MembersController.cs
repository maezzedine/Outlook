using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using backend.Areas.Identity;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;

        private static Position[] ArabicPositions = { Position.المحرر, Position.رئيس_تحرير, Position.رئيس_قسم, Position.عضو_سابق, Position.كاتب_صحفي, Position.مدقق_الموقع, Position.مدقق_النسخة, Position.مدقق_لغوي, Position.نائب_المحرر };
        private static Position[] EnglishPositions = { Position.Editor_In_Chief, Position.Senior_Editor, Position.Associate_Editor, Position.Junior_Editor, Position.Proofreader, Position.Copy_Editor, Position.Web_Editor, Position.Former_Member, Position.Staff_Writer  };

        public MembersController(OutlookContext context, UserManager<OutlookUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            GetJuniorEditorCategory(member);
            await GetMemberArticles(member);

            return GetMemberLanguage(member);
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetWriters()
        {
            var writers = from member in context.Member
                          where (member.Position == Position.Staff_Writer) || (member.Position == Position.كاتب_صحفي)
                          orderby member.Name
                          select GetMemberLanguage(member);

            return await writers.ToListAsync();
        }

        [HttpGet("board")]
        public async Task<ActionResult> GetBoardMembers()
        {
            var nonBoardMembers = new Position[] { Position.Staff_Writer, Position.Former_Member, Position.كاتب_صحفي, Position.عضو_سابق };

            var englishPositons = from position in EnglishPositions orderby position select position;
            var arabicPositons = from position in ArabicPositions orderby position select position;

            var boardmMembers = from member in context.Member
                                where !(nonBoardMembers.Contains(member.Position))
                                select member;

            await boardmMembers.ForEachAsync(m => GetJuniorEditorCategory(m));

            var englishBoardMembers = new Dictionary<string, IQueryable<Member>>();
            foreach (var position in englishPositons)
            {
                if (!nonBoardMembers.Contains(position))
                {
                    var members = from member in boardmMembers
                                  where member.Position == position
                                  select member;

                    englishBoardMembers[position.ToString().Replace('_', ' ')] = members;
                }
            }

            var arabicBoardMembers = new Dictionary<string, IQueryable<Member>>();
            foreach (var position in arabicPositons)
            {
                if (!nonBoardMembers.Contains(position))
                {
                    var members = from member in boardmMembers
                                  where member.Position == position
                                  select member;

                    arabicBoardMembers[position.ToString().Replace('_', ' ')] = members;
                }
            }

            return Ok(new
            {
                ArabicBoard = arabicBoardMembers,
                EnglishBoard = englishBoardMembers
            });
        }

        // GET: api/Members/top
        [HttpGet("top")]
        public ActionResult GetTopWriters()
        {
            var topWriters = from member in context.Member
                             orderby member.NumberOfArticles
                             descending
                             select GetMemberLanguage(member);

            var shortListedTopWriters = topWriters.AsEnumerable().Where(w => w.Language == Language.English).Take(3)
                .Concat(topWriters.AsEnumerable().Where(w => w.Language == Language.Arabic).Take(3));

            return Ok(shortListedTopWriters);
        }

        private static Member GetMemberLanguage(Member member)
        {
            if (ArabicPositions.Contains(member.Position))
            {
                member.Language = Language.Arabic;
            }
            else if (EnglishPositions.Contains(member.Position))
            {
                member.Language = Language.English;
            }
            return member;
        }

        private void GetJuniorEditorCategory(Member member)
        {
            if ((member.Position == Position.Junior_Editor) || (member.Position == Position.رئيس_قسم))
            {
                var categoryId = context.CategoryEditor.FirstOrDefault(c => c.MemberID == member.ID).CategoryID;
                var category = context.Category.Find(categoryId);
                member.Category = category;
            }
        }

        private async Task GetMemberArticles(Member member)
        {
            var articles = from article in context.Article
                           where article.MemberID == member.ID
                           select article;

            member.Articles = await articles.ToListAsync();

            var articlesController = new ArticlesController(context, userManager);
            member.Articles.ForEach(a =>
            {
                articlesController.GetArticleProperties(a).Wait();
            });
        }
    }
}
