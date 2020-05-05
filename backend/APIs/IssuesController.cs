using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly OutlookContext context;

        public IssuesController(OutlookContext context)
        {
            this.context = context;
        }

        // GET: api/Issues
        [HttpGet("{volumeId}")]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues(int volumeID)
        {
            var issues = from issue in context.Issue
                         where issue.VolumeID == volumeID
                         select issue;

            return await issues.ToListAsync();
        }

        // GET: api/Issues/5
        [HttpGet("Issue/{id}")]
        public async Task<ActionResult<Issue>> GetIssue(int id)
        {
            var issue = await context.Issue.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }
    }
}
