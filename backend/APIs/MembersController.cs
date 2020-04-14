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

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly OutlookContext context;

        private static Position[] ArabicPositions = { Position.المحرر, Position.رئيس_تحرير, Position.رئيس_قسم, Position.عضو_سابق, Position.كاتب_صحفي, Position.مدقق_الموقع, Position.مدقق_النسخة, Position.مدقق_لغوي, Position.نائب_المحرر };
        private static Position[] EnglishPositions = { Position.Associate_Editor, Position.Copy_Editor, Position.Editor_In_Chief, Position.Former_Member, Position.Junior_Editor, Position.Proofreader, Position.Senior_Editor, Position.Staff_Writer, Position.Web_Editor };

        public MembersController(OutlookContext context)
        {
            this.context = context;
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

            return member;
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
            var arabicBoardMembers = from member in context.ArabicBoard
                                     select member;

            var englishBoardMembers = from member in context.EnglishBoard
                                     select member;

            foreach (var arabicMember in arabicBoardMembers)
            {
                var member = await context.Member.FindAsync(arabicMember.MemberID);
                arabicMember.Member = member;
            }

            foreach (var englishMember in englishBoardMembers)
            {
                var member = await context.Member.FindAsync(englishMember.MemberID);
                englishMember.Member = member;
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
    }
}
