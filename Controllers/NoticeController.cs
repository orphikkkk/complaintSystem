using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SajhaSabal;
using SajhaSabal.Models;

namespace SajhaSabal.Controllers
{
    public class NoticeController : Controller
    {
        private readonly SsdbContext _context;

        public NoticeController(SsdbContext context)
        {
            _context = context;
        }

        // GET: Notice
        public async Task<IActionResult> Index()
        {
              return _context.Notices != null ? 
                          View(await _context.Notices.ToListAsync()) :
                          Problem("Entity set 'SsdbContext.Notices'  is null.");
        }

        // GET: Notice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notices == null)
            {
                return NotFound();
            }

            var noticeModel = await _context.Notices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeModel == null)
            {
                return NotFound();
            }

            return View(noticeModel);
        }

        // GET: Notice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] NoticeModel noticeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noticeModel);
        }

        // GET: Notice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notices == null)
            {
                return NotFound();
            }

            var noticeModel = await _context.Notices.FindAsync(id);
            if (noticeModel == null)
            {
                return NotFound();
            }
            return View(noticeModel);
        }

        // POST: Notice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] NoticeModel noticeModel)
        {
            if (id != noticeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeModelExists(noticeModel.Id))
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
            return View(noticeModel);
        }

        // GET: Notice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notices == null)
            {
                return NotFound();
            }

            var noticeModel = await _context.Notices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeModel == null)
            {
                return NotFound();
            }

            return View(noticeModel);
        }

        // POST: Notice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notices == null)
            {
                return Problem("Entity set 'SsdbContext.Notices'  is null.");
            }
            var noticeModel = await _context.Notices.FindAsync(id);
            if (noticeModel != null)
            {
                _context.Notices.Remove(noticeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeModelExists(int id)
        {
          return (_context.Notices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
