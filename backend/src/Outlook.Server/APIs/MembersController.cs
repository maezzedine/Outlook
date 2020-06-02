using AutoMapper;
using Outlook.Server.Data;
using Outlook.Server.Models.Dtos;
using Outlook.Server.Models.Interfaces;
using Outlook.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Server.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IMapper mapper;

        public MembersController(
            OutlookContext context, 
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
        public ActionResult<MemberDto> GetMember(int id)
        {
            var member = context.Member
                .Include(m => m.Articles)
                .ThenInclude(a => a.Category)
                .Include(m => m.Category)
                .FirstOrDefault(m => m.ID == id);

            if (member == null)
            {
                return NotFound();
            }

            return mapper.Map<MemberDto>(member);
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetWriters()
        {
            var writers = context.Member
                .Where(m => (m.Position == Position.Staff_Writer) || (m.Position == Position.كاتب_صحفي))
                .Include(m => m.Articles)
                .OrderBy(m => m.Name)
                .Select(m => mapper.Map<MemberDto>(m));

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
        public ActionResult GetBoardMembers()
        {
            var englishPositons = from position in MemberService.EnglishPositions orderby position select position;
            var arabicPositons = from position in MemberService.ArabicPositions orderby position select position;
            var nonBoardMembers = new List<Position> { Position.Staff_Writer, Position.Former_Member, Position.كاتب_صحفي, Position.عضو_سابق };

            //var boardMembers = context.Member
            //    .Include(m => m.Category)
            //    .AsEnumerable()
            //    .Where(m => !nonBoardMembers.Any(n => n == m.Position))
            //    .Select(m => mapper.Map<MemberDto>(m))
            //    .GroupBy(m => m.Language)
            //    .Select(g => g.GroupBy(m => m.Position, g => g));

            var boardMembers = context.Member
                .Include(m => m.Category)
                .AsEnumerable()
                .Where(m => !nonBoardMembers.Any(n => n == m.Position))
                .Select(m => mapper.Map<MemberDto>(m));

            // TODO: Improvement required
            var englishBoardMembers = new Dictionary<string, IEnumerable<MemberDto>>();
            MemberService.AddBoardMembers(englishBoardMembers, englishPositons, boardMembers);

            var arabicBoardMembers = new Dictionary<string, IEnumerable<MemberDto>>();
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
        public ActionResult GetTopWriters()
        {
            var members = context.Member
                .Include(m => m.Articles)
                .OrderByDescending(m => m.Articles.Count)
                .Select(m => mapper.Map<MemberDto>(m));

            var shortListedTopWriters = members.AsEnumerable().Where(w => w.Language == Language.English.ToString()).Take(3)
                .Concat(members.AsEnumerable().Where(w => w.Language == Language.Arabic.ToString()).Take(3));

            return Ok(shortListedTopWriters);
        }
    }
}
