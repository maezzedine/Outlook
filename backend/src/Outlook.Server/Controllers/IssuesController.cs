using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outlook.Server.Controllers
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
                         select _volume.Number;

            if (volume != null)
            {
                VolumeNumber = volume.FirstOrDefault();
            }

            var issues = context.Issue
                .Include(i => i.Volume)
                .Where(i => i.Volume.Id == id);

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
               .Include(i => i.Volume)
               .FirstOrDefaultAsync(i => i.Id == id);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Number,ArabicTheme,EnglishTheme")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return ValidationProblem(detail: "Volume Id cannot be null");
                }
                issue.Volume = await context.Volume.FindAsync(id);

                context.Add(issue);
                await context.SaveChangesAsync();

                var Volume = await context.Volume.FindAsync(id);

                logger.Log($"{HttpContext.User.Identity.Name} created Issue `{issue.Number}` in Volume {Volume.Number} ");

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

            var issue = await context.Issue
                .FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArabicTheme,EnglishTheme")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalIssue = await context.Issue
                        .Include(i => i.Volume)
                        .FirstOrDefaultAsync(i => i.Id == id);

                    originalIssue
                        .SetArabicTheme(issue.ArabicTheme)
                        .SetEnglishTheme(issue.EnglishTheme);

                    await context.SaveChangesAsync();
                    logger.Log($"{HttpContext.User.Identity.Name} editted Issue `{issue.Number}` in Volume {originalIssue.Volume.Number} ");

                    return RedirectToAction(nameof(Index), new { id = originalIssue.Volume.Id });
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
               .Include(i => i.Volume)
               .FirstOrDefaultAsync(i => i.Id == id);

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
            try
            {
                var issue = await context.Issue
                        .Include(i => i.Volume)
                        .FirstOrDefaultAsync(i => i.Id == id);

                var Volume = await context.Volume.FindAsync(issue.Volume.Id);
                logger.Log($"{HttpContext.User.Identity.Name} attempts to delete Issue `{issue.Number}` in Volume {Volume.Number}.");

                context.Issue.Remove(issue);
                await context.SaveChangesAsync();
                logger.Log($"Delete Completed.");

                return RedirectToAction(nameof(Index), new { id = issue.Volume.Id });
            }
            catch (DbUpdateException)
            {
                logger.Log($"Delete Failed, because of DbUpdateException.");

                var articles = context.Article
                    .Include(a => a.Issue)
                    .Where(a => a.Issue.Id == id)
                    .Select(a => a.Title);

                var errorMessage = "You cannot delete the following issue before deleting its articles: ";
                var errorDetail = new StringBuilder();
                foreach (var article in articles)
                {
                    errorDetail.Append($"{article} --- ");
                }

                return RedirectToAction("ServerError", "", new { message = errorMessage.ToString(), detail = errorDetail });
            }
        }

        private bool IssueExists(int id)
        {
            return context.Issue.Any(e => e.Id == id);
        }
    }
}
