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

namespace backend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IssuesController : Controller
    {
        private readonly OutlookContext context;

        public static int VolumeNumber;

        public IssuesController(OutlookContext context)
        {
            this.context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = from _volume in context.Volume
                         where _volume.Id == id
                         select _volume.VolumeNumber;

            if (volume.FirstOrDefault() != null)
            {
                VolumeNumber = volume.FirstOrDefault();
            }

            var issues = from issue in context.Issue
                           where issue.VolumeID == id
                           select issue;

            return View(await issues.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await context.Issue
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            issue.VolumeNumber = context.Volume.First(v => v.Id == issue.VolumeID).VolumeNumber;

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("IssueNumber,ar_pdf,en_pdf,ar_cover,en_cover")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return ValidationProblem(detail: "Volume Id cannot be null");
                }
                issue.VolumeID = (int) id;
                context.Add(issue);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = id });
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await context.Issue.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssueNumber,ar_pdf,en_pdf,ar_cover,en_cover")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(issue);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await context.Issue
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await context.Issue.FindAsync(id);
            context.Issue.Remove(issue);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return context.Issue.Any(e => e.Id == id);
        }
    }
}
