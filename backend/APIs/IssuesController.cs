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

        /// <summary>
        /// Gets the list of issues in a specific Volume
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/issues/1
        /// 
        /// </remarks>
        /// <param name="volumeID"></param>
        /// <returns>List of Issues</returns>
        /// <response code="200">Returns the list of Issues in a volume of given ID</response>
        [HttpGet("{volumeID}")]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues(int volumeID)
        {
            var issues = from issue in context.Issue
                         where issue.VolumeID == volumeID
                         select issue;

            return await issues.ToListAsync();
        }
    }
}
