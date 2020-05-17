using AutoMapper;
using backend.Data;
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
    public class CategoriesController : ControllerBase
    {
        private readonly OutlookContext context;
        private readonly IMapper mapper;

        public CategoriesController(
            IMapper mapper,
            OutlookContext context)
        {
            this.mapper = mapper;
            this.context = context;
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
        public ActionResult<List<CategoryDto>> GetCategories(int issueId)
        {
            var categories = context.Category
                .Include(c => c.JuniorEditors)
                .Include(c => c.Articles)
                .ThenInclude(a => a.Issue)
                .Select(c => mapper.Map<CategoryDto>(c))
                .ToList();

            foreach (var category in categories)
            {
                var articles = (category.Articles != null)? category.Articles.Where(a => a.Issue.Id == issueId).ToList() : null;
                category.Articles = articles;
            }

            return categories;
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
        public async Task<ActionResult<CategoryDto>>GetCategory(int id, int issueId)
        {
            var category = await context.Category
                .Include(c => c.Articles)
                .Include(c => c.JuniorEditors)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            category.Articles = category.Articles
                .AsEnumerable()
                .Where(a => a.IssueID == issueId)
                .ToList();

            return mapper.Map<CategoryDto>(category);
        }
    }
}
