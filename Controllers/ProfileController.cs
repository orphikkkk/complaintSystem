using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SajhaSabal.Models;

namespace SajhaSabal.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SsdbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(SsdbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profile
        public async Task<ActionResult> Index()
        {
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            var id = user.Id;
            var profile = await _context.UserDetails
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (profile == null)
            {
                return View(nameof(Create));
            }

            return View(profile);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,FirstName,LastName,Address,District,State,UserId")] UserDetailModel userDetail)
        {
            try
            {
                IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
                userDetail.UserId = user.Id;
                _context.Add(userDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetailModel = await _context.UserDetails.FindAsync(id);
            if (userDetailModel == null)
            {
                return NotFound();
            }

            return View(userDetailModel);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,FirstName,LastName,Address,District,State,UserId")] UserDetailModel userDetail)
        {
            if (id != userDetail.Id)
            {
                return NotFound();
            }
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            
            _context.Update(userDetail);
            userDetail.UserId = user.Id;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}