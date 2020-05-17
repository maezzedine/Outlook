using backend.Data;
using backend.Models;
using backend.Models.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin", AuthenticationSchemes = "Identity.Application")]
    public class CategoriesController : Controller
    {
        private readonly OutlookContext context;
        private readonly Logger.Logger logger;

        public CategoriesController(
            OutlookContext context)
        {
            this.context = context;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await context.Category
                .Include(c => c.JuniorEditors)
                .ToListAsync();

            return View(await context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await context.Category
                .Include(c => c.JuniorEditors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Tag")] Category category)
        {
            if (ModelState.IsValid)
            {
                context.Add(category);
                category.SetLanguage(Regex.IsMatch(category.CategoryName, @"^[a-zA-Z.\-\s]*$") ? Language.English : Language.Arabic);
                await context.SaveChangesAsync();

                logger.Log($"{HttpContext.User.Identity.Name} created Category `{category.CategoryName}` and ID `{category.Id}`.");

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await context.Category
                .FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Language,Tag")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            
            ModelState.Remove("CategoryName");
            if (ModelState.IsValid)
            {
                try
                {
                    var originalCategory = await context.Category
                        .FindAsync(category.Id);

                    originalCategory
                        .SetLanguage(category.Language)
                        .SetTag(category.Tag);

                    await context.SaveChangesAsync();

                    logger.Log($"{HttpContext.User.Identity.Name} editted Category `{category.CategoryName}`");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await context.Category
                .Include(c => c.JuniorEditors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await context.Category.FindAsync(id);

            logger.Log($"{HttpContext.User.Identity.Name} attempts to delete Category `{category.CategoryName}`");

            context.Category.Remove(category);
            await context.SaveChangesAsync();

            logger.Log($"Delete Completed.");

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return context.Category.Any(e => e.Id == id);
        }
    }
}
