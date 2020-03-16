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
    [Authorize(Roles = "Web-Editor, Editor-In-Chief, Admin")]
    public class VolumesController : Controller
    {
        private readonly OutlookContext context;
        private readonly IConfiguration config;

        public VolumesController(OutlookContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
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

                FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} created Volume {volume.VolumeNumber} ");

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,VolumeNumber,FallYear,SpringYear")] Volume volume)
        {
            if (id != volume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} editted Volume {volume.VolumeNumber}");
                    
                    context.Update(volume);
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
            
            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"{HttpContext.User.Identity.Name} admits to delete Volume {volume.VolumeNumber}");
            
            context.Volume.Remove(volume);
            
            FileLogger.FileLogger.Log(config.GetValue<string>("LogFilePath"), $"Delete Completed");
            
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool VolumeExists(int id)
        {
            return context.Volume.Any(e => e.Id == id);
        }
    }
}
