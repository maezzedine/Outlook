using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VolumeNumber,FallYear,SpringYear")] Volume volume)
        {
            if (ModelState.IsValid)
            {
                context.Add(volume);

                logger.Log($"{HttpContext.User.Identity.Name} created Volume {volume.VolumeNumber} ");

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), controllerName: "Home");
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

            var volume = await context.Volume.FindAsync(id);
            if (volume == null)
            {
                return NotFound();
            }
            return View(volume);
        }

        // POST: Volumes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FallYear,SpringYear")] Volume volume)
        {
            if (id != volume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldVolume = await context.Volume.FindAsync(volume.Id);
                    oldVolume.FallYear = volume.FallYear;
                    oldVolume.SpringYear = volume.SpringYear;

                    logger.Log($"{HttpContext.User.Identity.Name} editted Volume {volume.VolumeNumber}");
                    await context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
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
            var volume = await context.Volume.FindAsync(id);
            
            logger.Log($"{HttpContext.User.Identity.Name} admits to delete Volume {volume.VolumeNumber}");
            
            context.Volume.Remove(volume);
            
            logger.Log($"Delete Completed");
            
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool VolumeExists(int id)
        {
            return context.Volume.Any(e => e.Id == id);
        }
    }
}
