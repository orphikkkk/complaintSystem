using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SajhaSabal.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SajhaSabal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager  = roleManager;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            IdentityUser iUser = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber= model.PhoneNumber
                };
                
                await _userManager.CreateAsync(iUser, model.Password);
                return Redirect("/");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IdentityUser iuser = await _userManager.FindByNameAsync(model.UserName);
                if (iuser != null)
                {
                    bool result = await _userManager.CheckPasswordAsync(iuser, model.Password);
                    if (result)
                    {
                        await _signInManager.SignInAsync(iuser, result);
                        return Redirect("/");
                        // HttpContext.Session.SetString(iuser.Email, iuser.UserName);
                        // TempData[iuser.Email] = iuser;
                    }

                    ViewBag.Error = "Wrong Password";
                    return View();

                    // TempData.Keep("");

                    // return RedirectToAction("/");
                }
                ViewBag.Error = "No User";
                return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("Index", "Home"); 
        }

        [Authorize (Roles = "Admin")]
        [HttpGet]
        public IActionResult Roles()
        {
            List <SelectListItem> roles = _roleManager.Roles.Select(x=>new SelectListItem { Text=x.Name,Value=x.Name}).ToList();
            
            ViewBag.Roles = roles;
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Roles(RoleModel model)
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Name));
            return RedirectToAction("Roles");
        }
        
        [Authorize (Roles = "Admin")]
        [HttpGet]
        public IActionResult AssignRole()
        {
            List <SelectListItem> roles= _roleManager.Roles.Select(x=>new SelectListItem { Text=x.Name,Value=x.Name}).ToList();
            List <SelectListItem> users=_userManager.Users.Select(x=>new SelectListItem { Text=x.UserName, Value=x.Id}).ToList();
            ViewBag.Roles = roles;
            ViewBag.Users = users;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleModel model)
        {
            IdentityUser user = await _userManager.FindByIdAsync(model.UserId);
            await _userManager.AddToRoleAsync(user, model.RoleName);
            return RedirectToAction("AssignRole");

        }
    }

    
}