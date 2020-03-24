using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin", AuthenticationSchemes = "Identity.Application")]
    public class CategoriesController : Controller
    {
        private readonly OutlookContext context;
        private readonly IConfiguration config;

        public CategoriesController(OutlookContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await context.Category.ToListAsync();

            foreach (var category in categories)
            {
                category.JuniorEditors = new List<Member>();

                var editorIDs = from categoryEditor in context.CategoryEditor
                                where categoryEditor.CategoryID == category.Id
                                select categoryEditor.MemberID;

                // Usually there are 1 or 2 editors
                foreach (var editorID in editorIDs)
                {
                    var editor = await context.Member.FindAsync(editorID);
                    category.JuniorEditors.Add(editor);
                }
            }

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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.JuniorEditors = new List<Member>();

            var editorIDs = from categoryEditor in context.CategoryEditor
                            where categoryEditor.CategoryID == category.Id
                            select categoryEditor.MemberID;

            // Usually there are 1 or 2 editors
            foreach (var editorID in editorIDs)
            {
                var editor = await context.Member.FindAsync(editorID);
                category.JuniorEditors.Add(editor);
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Language,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                context.Add(category);
                await context.SaveChangesAsync();

                FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} created Category `{category.CategoryName}` and ID `{category.Id}`.");

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

            var category = await context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Language,CategoryName")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} editted Category `{category.CategoryName}`");
                    context.Update(category);
                    await context.SaveChangesAsync();
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.JuniorEditors = new List<Member>();

            var editorIDs = from categoryEditor in context.CategoryEditor
                            where categoryEditor.CategoryID == category.Id
                            select categoryEditor.MemberID;

            // Usually there are 1 or 2 editors
            foreach (var editorID in editorIDs)
            {
                var editor = await context.Member.FindAsync(editorID);
                category.JuniorEditors.Add(editor);
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
            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} attempts to delete Category `{category.CategoryName}`");
            context.Category.Remove(category);
            await context.SaveChangesAsync();
            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"Delete Completed.");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return context.Category.Any(e => e.Id == id);
        }
    }
}
