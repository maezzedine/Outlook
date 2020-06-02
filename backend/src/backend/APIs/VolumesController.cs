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
    public class VolumesController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IMapper mapper;

        public VolumesController(
            OutlookContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<VolumeDto>>> GetVolume()
        {
            return await context.Volume
                .Select(v => mapper.Map<VolumeDto>(v))
                .ToListAsync();
        }
    }
}
