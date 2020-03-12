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

namespace backend.Controllers
{
    [Authorize(Roles = "Admin, Editor-In-Chief")]
    public class MembersController : Controller
    {
        private readonly OutlookContext context;

        public static List<string> Categories;
        
        public MembersController(OutlookContext context)
        {
            this.context = context;
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

            return View(await members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int id)
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
        public async Task<IActionResult> Create([Bind("Name,NumberOfArticles,NumberOfComments,NumberOfReactions,Position,CategoryField")] Member member)
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

                if (member.Position == Position.Junior_Editor || member.Position == Position.رئيس_قسم)
                {
                    // Creating new CategoryEditor relation instance
                    var categoryID = context.Category.First(c => c.CategoryName == member.CategoryField).Id;
                    context.CategoryEditor.Add(new CategoryEditorRelation { CategoryID = categoryID, MemberID = member.ID });
                }

                await context.SaveChangesAsync();
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
        public async Task<IActionResult> Edit(int id, [Bind("Name,Position")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(member);
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
        public async Task<IActionResult> Delete(int id)
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

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await context.Member.FindAsync(id);
            context.Member.Remove(member);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return context.Member.Any(e => e.ID == id);
        }
    }
}
