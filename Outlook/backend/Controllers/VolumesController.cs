using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    public class VolumesController : Controller
    {
        private readonly OutlookContext _context;

        public VolumesController(OutlookContext context)
        {
            _context = context;
        }

        // GET: Volumes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Volume.ToListAsync());
        }

        // GET: Volumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.Volume
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
                _context.Add(volume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var volume = await _context.Volume.FindAsync(id);
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
                    _context.Update(volume);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.Volume
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volume = await _context.Volume.FindAsync(id);
            _context.Volume.Remove(volume);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolumeExists(int id)
        {
            return _context.Volume.Any(e => e.Id == id);
        }
    }
}
