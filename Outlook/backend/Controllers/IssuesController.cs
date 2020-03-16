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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin")]
    public class IssuesController : Controller
    {
        private readonly OutlookContext context;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        public static int VolumeNumber;

        public IssuesController(OutlookContext context, IWebHostEnvironment env, IConfiguration config)
        {
            this.context = context;
            this.env = env;
            this.config = config;
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
        public async Task<IActionResult> Create(int? id, [Bind("IssueNumber,ArabicPDF,EnglishPDF")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return ValidationProblem(detail: "Volume Id cannot be null");
                }
                issue.VolumeID = (int) id;

                if (issue.ArabicPDF != null)
                {
                    issue.ar_pdf = await CopyPdfFileToLocal(issue.ArabicPDF);
                }

                if (issue.EnglishPDF != null)
                {
                    issue.en_pdf = await CopyPdfFileToLocal(issue.EnglishPDF);
                }

                context.Add(issue);
                await context.SaveChangesAsync();

                var Volume = await context.Volume.FindAsync(id);

                FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} created Issue `{issue.IssueNumber}` in Volume {Volume.VolumeNumber} ");

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssueNumber,ArabicPDF,EnglishPDF")] Issue issue)
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

                    // Update Arabic PDF file of the issue
                    if (issue.ArabicPDF != null)
                    {
                        if (oldIssueVersion.ar_pdf != null)
                        {
                            // Delete the old pdf from the server
                            var path = env.WebRootPath + oldIssueVersion.ar_pdf;
                            System.IO.File.Delete(path);
                        }
                        oldIssueVersion.ar_pdf = await CopyPdfFileToLocal(issue.ArabicPDF);
                    }

                    // Update Arabic PDF file of the issue
                    if (issue.EnglishPDF != null)
                    {
                        if (oldIssueVersion.en_pdf != null)
                        {
                            // Delete the old pdf from the server
                            var path = env.WebRootPath + oldIssueVersion.en_pdf;
                            System.IO.File.Delete(path);
                        }
                        oldIssueVersion.en_pdf = await CopyPdfFileToLocal(issue.EnglishPDF);
                    }
                    var Volume = await context.Volume.FindAsync(issue.VolumeID);

                    FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} editted Issue `{issue.IssueNumber}` in Volume {Volume.VolumeNumber} ");

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
            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} attempts to delete Issue `{issue.IssueNumber}` in Volume {Volume.VolumeNumber} ");

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
