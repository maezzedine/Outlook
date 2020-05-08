using backend.Areas.Identity;
using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly ArticleService articleService;
        private readonly MemberService memberService;

        public MembersController(
            OutlookContext context, 
            MemberService memberService,
            ArticleService articleService)
        {
            this.context = context;
            this.memberService = memberService;
            this.articleService = articleService;
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMember(int id)
        {
            var member = await context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            memberService.GetMemberLanguageAndArticlesCount(member);
            memberService.GetJuniorEditorCategory(member);

            var articles = from article in context.Article
                           where article.MemberID == member.ID
                           select article;

            foreach (var article in articles)
            {
                await articleService.GetArticleProperties(article);
            }

            return Ok(new
            {
                member = member,
                articles = articles
            });
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetWriters()
        {
            var writers = from member in context.Member
                          where (member.Position == Position.Staff_Writer) || (member.Position == Position.كاتب_صحفي)
                          orderby member.Name
                          select member;

            foreach (var writer in writers)
            {
                memberService.GetMemberLanguageAndArticlesCount(writer);
            }

            return await writers.ToListAsync();
        }

        [HttpGet("board")]
        public async Task<ActionResult> GetBoardMembers()
        {

            var englishPositons = from position in MemberService.EnglishPositions orderby position select position;
            var arabicPositons = from position in MemberService.ArabicPositions orderby position select position;

            var boardMembers = from member in context.Member
                               where !(MemberService.NonBoardMembers.Contains(member.Position))
                               select member;

            await boardMembers.ForEachAsync(m => memberService.GetJuniorEditorCategory(m));

            var englishBoardMembers = new Dictionary<string, IQueryable<Member>>();
            MemberService.AddBoardMembers(englishBoardMembers, englishPositons, boardMembers);

            var arabicBoardMembers = new Dictionary<string, IQueryable<Member>>();
            MemberService.AddBoardMembers(arabicBoardMembers, arabicPositons, boardMembers);

            return Ok(new
            {
                ArabicBoard = arabicBoardMembers,
                EnglishBoard = englishBoardMembers
            });
        }

        // GET: api/Members/top
        [HttpGet("top")]
        public async Task<ActionResult> GetTopWriters()
        {
            var members = from member in context.Member
                          select member;

            foreach (var writer in members)
            {
                memberService.GetMemberLanguageAndArticlesCount(writer);
            }

            var writers = await members.ToListAsync();

            var topWriters = from member in writers
                             orderby member.NumberOfArticles descending
                             select member;

            var shortListedTopWriters = topWriters.AsEnumerable().Where(w => w.Language == Language.English).Take(3)
                .Concat(topWriters.AsEnumerable().Where(w => w.Language == Language.Arabic).Take(3));

            return Ok(shortListedTopWriters);
        }

    }
}
