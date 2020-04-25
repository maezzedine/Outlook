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
using backend.Models.Interfaces;
using backend.Models.Relations;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [Authorize(Roles = "Admin, Editor-In-Chief", AuthenticationSchemes = "Identity.Application")]
    public class MembersController : Controller
    {
        private readonly OutlookContext context;
        private readonly Logger.Logger logger;
        public static List<string> Categories;
        
        public MembersController(OutlookContext context)
        {
            this.context = context;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            // Save the List of categories names to be accessed
            var categories = from category in context.Category
                             select category.CategoryName;

            Categories = await categories.ToListAsync();
            
            var members = from member in context.Member
                          where (member.Position != Position.كاتب_صحفي) && (member.Position != Position.Staff_Writer)
                          select member;

            foreach (var member in members)
            {
                if (isJuniorEditor(member))
                {
                    var categoryEditor = await context.CategoryEditor.FirstAsync(ce => ce.MemberID == member.ID);
                    var category = await context.Category.FirstAsync(c => c.Id == categoryEditor.CategoryID);
                    member.CategoryField = category.CategoryName;
                }
                else
                {
                    member.CategoryField = "";
                }
            }

            return View(await members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member
                .FirstOrDefaultAsync(m => m.ID == id);

            if (member == null)
            {
                return NotFound();
            }

            if (isJuniorEditor(member))
            {
                var categoryEditor = await context.CategoryEditor.FirstAsync(ce => ce.MemberID == member.ID);
                var category = await context.Category.FirstAsync(c => c.Id == categoryEditor.CategoryID);
                member.CategoryField = category.CategoryName;
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Position,CategoryField")] Member member)
        {
            if (ModelState.IsValid)
            {
                var memberExists = await context.Member.FirstOrDefaultAsync(m => m.Name == member.Name);

                if (memberExists != null)
                {
                    return ValidationProblem(detail: "Member name already exists.");
                }

                context.Add(member);
                await context.SaveChangesAsync();

                if (isJuniorEditor(member))
                {
                    // Creating new CategoryEditor relation instance
                    var categoryID = context.Category.First(c => c.CategoryName == member.CategoryField).Id;
                    context.CategoryEditor.Add(new CategoryEditorRelation { CategoryID = categoryID, MemberID = member.ID });
                }

                await context.SaveChangesAsync();

                logger.Log($"{HttpContext.User.Identity.Name} created member `{member.Name}` with position {member.Position} " +
                    $"(Category = {member.CategoryField}) ");

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member.FindAsync(id);

            if (isJuniorEditor(member))
            {
                var categoryEditor = await context.CategoryEditor.FirstAsync(ce => ce.MemberID == member.ID);
                var categoryID = categoryEditor.CategoryID;
                var categoryName = (await context.Category.FindAsync(categoryID)).CategoryName;
                member.CategoryField = categoryName;
            }

            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Position,CategoryField")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldMemberData = await context.Member.FindAsync(member.ID);


                    if (isJuniorEditor(oldMemberData))
                    {
                        var oldCategoryEditorData = await context.CategoryEditor.FirstOrDefaultAsync(ce => ce.MemberID == oldMemberData.ID);
                        var oldCategory = await context.Category.FindAsync(oldCategoryEditorData.CategoryID);

                        logger.Log($"{HttpContext.User.Identity.Name} editted member `{oldMemberData.Name}` with position {oldMemberData.Position} " +
                            $"(Category = {oldCategory.CategoryName}) ");

                        // Delete CategoryEditor relation if needed
                        if ((member.Position != Position.Junior_Editor) && (member.Position != Position.رئيس_قسم))
                        {
                            context.CategoryEditor.Remove(oldCategoryEditorData);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            var newCategory = await context.Category.FirstOrDefaultAsync(c => c.CategoryName == member.CategoryField);

                            if (newCategory != null)
                            {
                                var newCategoryID = newCategory.Id;
                                // Update CategoryEditor relation if needed
                                if (newCategoryID != oldCategoryEditorData.CategoryID)
                                {
                                    oldCategoryEditorData.CategoryID = newCategoryID;
                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isJuniorEditor(member))
                        {
                            var newCategory = await context.Category.FirstOrDefaultAsync(c => c.CategoryName == member.CategoryField);
                            
                            // Create CategoryEditor relation if needed
                            if (newCategory != null)
                            {
                                var newCategoryID = newCategory.Id;
                                context.CategoryEditor.Add(new CategoryEditorRelation { CategoryID = newCategoryID, MemberID = member.ID });
                                await context.SaveChangesAsync();
                            }
                        }

                    }

                    oldMemberData.Position = member.Position;

                    logger.Log($"to become: Name: {member.Name}\n" +
                        $"Position: {member.Position}\n" +
                        $"Category: {member.CategoryField}");

                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            if (isJuniorEditor(member))
            {
                var categoryEditor = await context.CategoryEditor.FirstAsync(ce => ce.MemberID == member.ID);
                var category = await context.Category.FirstAsync(c => c.Id == categoryEditor.CategoryID);
                member.CategoryField = category.CategoryName;
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var member = await context.Member.FindAsync(id);

            logger.Log($"{HttpContext.User.Identity.Name} admits to delete member `{member.Name}`");

            context.Member.Remove(member);
            await context.SaveChangesAsync();

            logger.Log($"Delete Completed.");

            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return context.Member.Any(e => e.ID == id);
        }

        public static bool isJuniorEditor(Member member) => (member.Position == Position.Junior_Editor) || (member.Position == Position.رئيس_قسم);
    }
}
