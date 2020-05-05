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

        // GET: api/Volumes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Volume>>> GetVolume()
        {
            return await context.Volume.ToListAsync();
        }
    }
}
