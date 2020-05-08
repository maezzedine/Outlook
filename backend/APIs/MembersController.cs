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

        /// <summary>
        /// Gets member given their ID
        /// </summary>
        /// <remarks>
        /// The member can be an outlook staff, former staff or a writer
        /// 
        /// Sample request:
        /// 
        ///     GET /api/members/1
        /// 
        /// </remarks>
        /// <param name="id">Member ID</param>
        /// <returns>A JSON object containg the member and their articles</returns>
        /// <response code="200">Returns the specified member</response>
        /// <response code="404">Returns NotFound result if no member with the given ID was found</response>
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

        /// <summary>
        /// Gets the list of all writers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/members
        ///     
        /// </remarks>
        /// <returns>List of members</returns>
        /// <response code="200">Returns the list of members</response>
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

        /// <summary>
        /// Gets the current board members
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/members/board
        /// 
        /// </remarks>
        /// <returns>JSON object with the keys ArabicBoard and EnglishBoard</returns>
        /// <response code="200">Returns a JSON object containing the Enlish and the Arabic board members</response>
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

        /// <summary>
        /// Gets the statistics of the most contributing writers with their articles count
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/members/top
        /// 
        /// </remarks>
        /// <returns>List of Arabic and English writers</returns>
        /// <response code="200">Returns the list of writers</response>
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
