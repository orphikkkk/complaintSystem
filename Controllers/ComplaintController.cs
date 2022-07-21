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
    public class ComplaintController : Controller
    {
        private readonly SsdbContext _context;

        public ComplaintController(SsdbContext context)
        {
            _context = context;
        }

        // GET: Complaint
        public async Task<IActionResult> Index()
        {
              return _context.Complaints != null ? 
                          View(await _context.Complaints.ToListAsync()) :
                          Problem("Entity set 'SsdbContext.Complaints'  is null.");
        }

        // GET: Complaint/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaintModel = await _context.Complaints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintModel == null)
            {
                return NotFound();
            }

            return View(complaintModel);
        }

        // GET: Complaint/Create
        public IActionResult Create()
        {
            List<DepartmentModel> department = _context.Departments.ToList();
            ViewBag.Departments = department;
            return View();
        }

        // POST: Complaint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Status,DepartmentId,UserId")] ComplaintModel complaintModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintModel);
        }

        // GET: Complaint/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaintModel = await _context.Complaints.FindAsync(id);
            if (complaintModel == null)
            {
                return NotFound();
            }
            return View(complaintModel);
        }

        // POST: Complaint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,DepartmentId,UserId")] ComplaintModel complaintModel)
        {
            if (id != complaintModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintModelExists(complaintModel.Id))
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
            return View(complaintModel);
        }

        // GET: Complaint/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaintModel = await _context.Complaints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintModel == null)
            {
                return NotFound();
            }

            return View(complaintModel);
        }

        // POST: Complaint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'SsdbContext.Complaints'  is null.");
            }
            var complaintModel = await _context.Complaints.FindAsync(id);
            if (complaintModel != null)
            {
                _context.Complaints.Remove(complaintModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintModelExists(int id)
        {
          return (_context.Complaints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
