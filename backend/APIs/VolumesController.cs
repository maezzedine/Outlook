using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumesController : ControllerBase
    {
        private readonly OutlookContext context;

        public VolumesController(OutlookContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the list of available volumes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/volumes
        /// 
        /// </remarks>
        /// <returns>List of volumes</returns>
        /// <response code="200">Returns the list of volumes</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Volume>>> GetVolume()
        {
            return await context.Volume.ToListAsync();
        }
    }
}
