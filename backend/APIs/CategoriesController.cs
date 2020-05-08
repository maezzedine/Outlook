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
        private readonly CategoryService categoryService;

        public CategoriesController(
            OutlookContext context,
            CategoryService categoryService)
        {
            this.context = context;
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Gets the list of categories and their articles count in a specific issue
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/categories/1
        /// 
        /// </remarks>
        /// <param name="issueId"></param>
        /// <returns>List of catgories</returns>
        /// <response code="200">Returns the list of categories</response>
        [HttpGet("{issueId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(int issueId)
        {
            var categories = context.Category;

            foreach (var category in categories)
            {
                await categoryService.GetCategoryDetails(category, issueId);
            }

            return await categories.ToListAsync();
        }

        /// <summary>
        /// Gets a specific category and its articles count in a specific issue
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/categories/1/2
        /// </remarks>
        /// <param name="id">category ID</param>
        /// <param name="issueId"></param>
        /// <returns>A category</returns>
        /// <response code="200">Returns the category with its properties</response>
        /// <response code="404">Returns NotFound result if no category with the given ID was found</response>
        [HttpGet("{id}/{issueId}")]
        public async Task<ActionResult<Category>> GetCategory(int id, int issueId)
        {
            var category = await context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            await categoryService.GetCategoryDetails(category, issueId);

            return category;
        }
    }
}
