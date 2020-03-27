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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var categories = context.Category;

            foreach (var category in categories)
            {
                category.TagName = category.Tag.ToString();
            }

            return await categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.JuniorEditors = new List<Member>();

            // Get the IDs of the editors of this category
            var categoryEdotorIDs = from categoryEditor in context.CategoryEditor
                                    where categoryEditor.CategoryID == id
                                    select categoryEditor.MemberID;

            // Add each editor to the list of editors in the category object
            foreach (var categoryEditorID in categoryEdotorIDs)
            {
                var categoryEditor = await context.Member.FindAsync(categoryEditorID);
                category.JuniorEditors.Add(categoryEditor);
            }

            return category;
        }
    }
}
