using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly OutlookContext context;

        public MembersController(OutlookContext context)
        {
            this.context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await context.Member.ToListAsync();
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

        [HttpGet]
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
    }
}
