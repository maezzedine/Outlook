using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using Outlook.Models.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Server.Controllers
{
    [Authorize(Roles = "Admin, Editor-In-Chief, Web-Editor", AuthenticationSchemes = "Identity.Application")]
    public class WritersController : Controller
    {
        private readonly OutlookContext context;
        private readonly Logger.Logger logger;

        public WritersController(OutlookContext context)
        {
            this.context = context;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);
        }
        // GET: Writers
        public async Task<ActionResult> Index()
        {
            var writers = from member in context.Member
                          where (member.Position == OutlookConstants.Position.Staff_Writer) || (member.Position == OutlookConstants.Position.كاتب_صحفي)
                          select member;

            return View(await writers.ToListAsync());
        }

        // GET: Writers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member
                .Include(w => w.Category)
                .Include(w => w.Articles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Writers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Writers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                context.Add(member);
                await context.SaveChangesAsync();

                logger.Log($"{HttpContext.User.Identity.Name} created writer `{member.Name}`");

                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Writers/Edit/5
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

        // POST: Writers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalWriter = context.Member
                        .Find(id);

                    if (originalWriter.Position != member.Position)
                    {
                        originalWriter.Position = member.Position;
                        await context.SaveChangesAsync();
                        logger.Log($"{HttpContext.User.Identity.Name} eddited writer `{originalWriter.Name}` to become `{member.Position}`");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WriterExists(member.Id))
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
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await context.Member
                .Include(w => w.Category)
                .Include(w => w.Articles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var member = await context.Member.FindAsync(id);

            logger.Log($"{HttpContext.User.Identity.Name} admits to delete writer `{member.Name}`");
            context.Member.Remove(member);
            await context.SaveChangesAsync();
            logger.Log("Delet Completed.");

            return RedirectToAction(nameof(Index));
        }

        private bool WriterExists(int id)
        {
            return context.Member.Any(e => e.Id == id);
        }

    }
}