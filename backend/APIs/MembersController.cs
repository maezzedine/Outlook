using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using backend.Areas.Identity;
using backend.Services;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly UserManager<OutlookUser> userManager;

        
        public MembersController(OutlookContext context, UserManager<OutlookUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
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

            MemberService.GetMemberLanguage(member);
            MemberService.GetJuniorEditorCategory(member, context);

            var articles = from article in context.Article
                           where article.MemberID == member.ID
                           select article;

            foreach (var article in articles)
            {
                await ArticleService.GetArticleProperties(article, context);
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
                          where MemberService.IsWriter(member)
                          orderby member.Name
                          select MemberService.GetMemberLanguage(member);

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

            await boardMembers.ForEachAsync(m => MemberService.GetJuniorEditorCategory(m, context));

            var englishBoardMembers = new Dictionary<string, IQueryable<Member>>();
            MemberService.AddBoardMembers(englishBoardMembers, englishPositons, boardMembers);

            var arabicBoardMembers = new Dictionary<string, IQueryable<Member>>();
            MemberService.AddBoardMembers(arabicBoardMembers, englishPositons, boardMembers);

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
                             select MemberService.GetMemberLanguage(member);

            var shortListedTopWriters = topWriters.AsEnumerable().Where(w => w.Language == Language.English).Take(3)
                .Concat(topWriters.AsEnumerable().Where(w => w.Language == Language.Arabic).Take(3));

            return Ok(shortListedTopWriters);
        }
        
    }
}
