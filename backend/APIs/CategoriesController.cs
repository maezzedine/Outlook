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
                await getCategoryDetails(category, issueId);
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

            await getCategoryDetails(category, issueId);

            return category;
        }

        private async Task getCategoryDetails(Category category, int issueId)
        {
            category.TagName = category.Tag.ToString();
            category.ArticlesCount = context.Article.Where(a => (a.CategoryID == category.Id) && (a.IssueID == issueId)).Count();

            // Find junior editors of category
            var juniorEditorsIDs = from categoryEditor in context.CategoryEditor
                                   where categoryEditor.CategoryID == category.Id
                                   select categoryEditor.MemberID;

            var juniorEditors = from member in context.Member
                                where juniorEditorsIDs.Contains(member.ID)
                                select member;

            category.JuniorEditors = await juniorEditors.ToListAsync();
            return;
        }
    }
}
