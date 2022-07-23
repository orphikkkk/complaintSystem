using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;


        public ComplaintController(SsdbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Complaint
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.id = user.Id;
            }

            return _context.Complaints != null
                ? View(await _context.Complaints.ToListAsync())
                : Problem("Entity set 'SsdbContext.Complaints'  is null.");
        }

        // GET: Complaint/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            ComplaintViewModel complaint = (from c in _context.Complaints
                join d in _context.Departments on c.DepartmentId equals d.Id
                //where p.IsActive == true
                select new ComplaintViewModel
                {
                    Title = c.Title,
                    Id = c.Id,
                    Status = c.Status,
                    DepartmentId = c.DepartmentId,
                    Description = c.Description,
                    DepartmentName = d.Title
                }).First(c => c.Id == id);

            return View(complaint);
        }

        // GET: Complaint/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }

            List<DepartmentModel> department = _context.Departments.ToList();
            ViewBag.Departments = department;

            return View();
        }

        // POST: Complaint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,Status,DepartmentId,UserId")] ComplaintModel complaintModel)
        {
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            complaintModel.UserId = user.Id;
            complaintModel.Status = "Processing";
            _context.Add(complaintModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Title,Description,Status,DepartmentId,UserId")] ComplaintModel complaintModel)
        {
            if (id != complaintModel.Id)
            {
                return NotFound();
            }
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            

            try
            {
                _context.Update(complaintModel);
                complaintModel.UserId = user.Id;
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

        public IActionResult Delete(int id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'SsdbContext.Complaints'  is null.");
            }

            ComplaintModel complaintModel = _context.Complaints.FirstOrDefault(X => X.Id == id);
            if (complaintModel != null)
            {
                _context.Complaints.Remove(complaintModel);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintModelExists(int id)
        {
            return (_context.Complaints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}