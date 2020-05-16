using AutoMapper;
using backend.Data;
using backend.Models;
using backend.Models.Dtos;
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
        private readonly IMapper mapper;

        public IssuesController(
            OutlookContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssues(int volumeID)
        {
            var issues = context.Issue
                .Where(i => i.VolumeID == volumeID)
                .Select(i => mapper.Map<IssueDto>(i));

            return await issues.ToListAsync();
        }
    }
}
