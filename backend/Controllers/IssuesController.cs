using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace backend.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin", AuthenticationSchemes = "Identity.Application")]
    public class IssuesController : Controller
    {
        private readonly OutlookContext context;
        private readonly IWebHostEnvironment env;
        private readonly Logger.Logger logger;
        public static int VolumeNumber;

        public IssuesController(OutlookContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);
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

            if (volume != null)
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
        public async Task<IActionResult> Create(int? id, [Bind("IssueNumber,ArabicTheme,EnglishTheme,ArabicPDF,EnglishPDF")] Issue issue)
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

                var Volume = await context.Volume.FindAsync(id);

                logger.Log($"{HttpContext.User.Identity.Name} created Issue `{issue.IssueNumber}` in Volume {Volume.VolumeNumber} ");

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssueNumber,ArabicTheme,EnglishTheme,ArabicPDF,EnglishPDF")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldIssueVersion = await context.Issue.FindAsync(id);

                    var Volume = await context.Volume.FindAsync(issue.VolumeID);

                    logger.Log($"{HttpContext.User.Identity.Name} editted Issue `{issue.IssueNumber}` in Volume {Volume.VolumeNumber} ");

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
        [Authorize(Roles = "Editor-In-Chief, Admin")]
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
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await context.Issue.FindAsync(id);
            var VolumeID = issue.VolumeID;

            var Volume = await context.Volume.FindAsync(VolumeID);
            logger.Log($"{HttpContext.User.Identity.Name} attempts to delete Issue `{issue.IssueNumber}` in Volume {Volume.VolumeNumber} ");

            context.Issue.Remove(issue);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = VolumeID });
        }

        private bool IssueExists(int id)
        {
            return context.Issue.Any(e => e.Id == id);
        }

        private async Task<string> CopyPdfFileToLocal(IFormFile file)
        {
            // Add unique name to avoid possible name conflicts
            var uniquePdfName = DateTime.Now.Ticks.ToString() + ".pdf";
            var issuePdfFolderPath = Path.Combine(new string[] { env.WebRootPath, "pdf", "Issues\\" });
            var issuePdfFilePath = Path.Combine(issuePdfFolderPath, uniquePdfName);
            if (!Directory.Exists(issuePdfFolderPath))
            {
                Directory.CreateDirectory(issuePdfFolderPath);
            }
            using (var fileStream = new FileStream(issuePdfFilePath, FileMode.Create, FileAccess.Write))
            {
                // Copy the pdf file to storage
                await file.CopyToAsync(fileStream);
            }

            // Save pdf local path in the issue object
            var pdfFilePath = @"/pdf/Issues/" + uniquePdfName;

            return pdfFilePath;
        }
    }
}
