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
