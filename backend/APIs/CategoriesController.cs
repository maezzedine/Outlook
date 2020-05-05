using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly OutlookContext context;

        public CategoriesController(OutlookContext context)
        {
            this.context = context;
        }

        // GET: api/Categories
        [HttpGet("{issueId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(int issueId)
        {
            var categories = context.Category;

            foreach (var category in categories)
            {
                await CategoryService.GetCategoryDetails(category, issueId, context);
            }

            return await categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}/{issueId}")]
        public async Task<ActionResult<Category>> GetCategory(int id, int issueId)
        {
            var category = await context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            await CategoryService.GetCategoryDetails(category, issueId, context);

            return category;
        }
    }
}
