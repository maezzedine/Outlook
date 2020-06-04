using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outlook.Models.Core.Models;
using Outlook.Models.Data;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outlook.Server.Controllers
{
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin", AuthenticationSchemes = "Identity.Application")]
    public class VolumesController : Controller
    {
        private readonly OutlookContext context;
        private readonly Logger.Logger logger;

        public VolumesController(OutlookContext context)
        {
            this.context = context;
            logger = Logger.Logger.Instance(Logger.Logger.LogField.server);
        }

        // GET: Volumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await context.Volume
                .FirstOrDefaultAsync(m => m.Id == id);

            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        // GET: Volumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Volumes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,FallYear,SpringYear")] Volume volume)
        {
            if (ModelState.IsValid)
            {
                context.Add(volume);
                await context.SaveChangesAsync();
                logger.Log($"{HttpContext.User.Identity.Name} created Volume {volume.Number}.");

                return RedirectToAction(nameof(Index), "Home");
            }
            return View(volume);
        }

        // GET: Volumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await context.Volume
                .FindAsync(id);

            if (volume == null)
            {
                return NotFound();
            }
            return View(volume);
        }

        // POST: Volumes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Volume volume)
        {
            if (id != volume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalVolume = await context.Volume
                        .FindAsync(volume.Id);

                    originalVolume
                        .SetFallYear(volume.FallYear)
                        .SetSpringYear(volume.SpringYear);

                    await context.SaveChangesAsync();
                    logger.Log($"{HttpContext.User.Identity.Name} editted Volume {originalVolume.Number}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolumeExists(volume.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(volume);
        }

        // GET: Volumes/Delete/5
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await context.Volume
                .FirstOrDefaultAsync(m => m.Id == id);

            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        // POST: Volumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor-In-Chief, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var volume = await context.Volume
                        .FindAsync(id);

                logger.Log($"{HttpContext.User.Identity.Name} admits to delete Volume {volume.Number}.");
                context.Volume.Remove(volume);
                await context.SaveChangesAsync();
                logger.Log($"Delete Completed.");

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (DbUpdateException)
            {
                logger.Log($"Delete Failed, because of DbUpdateException.");

                var issues = context.Issue
                    .Include(i => i.Volume)
                    .Where(i => i.Volume.Id == id)
                    .Select(i => i.Number);

                var errorMessage = "You cannot delete the following volume before deleting its issues: ";
                var errorDetail = new StringBuilder();
                foreach (var issue in issues)
                {
                    errorDetail.Append($"{issue} --- ");
                }

                return RedirectToAction("ServerError", "", new { message = errorMessage.ToString(), detail = errorDetail });
            }
        }

        private bool VolumeExists(int id)
        {
            return context.Volume.Any(e => e.Id == id);
        }
    }
}
