using CMSProject.Entity.Entities.Concrete;
using CMSProject.Web.Areas.Admin.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager, // UserManager =>  a’dan z’ye kullanıcı yönetimini gerçekleştirmemizi sağlayan sınıftır.
                              RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index() => View(_roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken] // “ValidateAntiForgeryToken” kısaca .NET platformunun dış saldırılara karşı aldığı, bilgi isteyen kişi gerçekten sen misin diye kontrol eden önlemidir.
        public async Task<IActionResult> Create([MinLength(2,ErrorMessage ="Minimum 2 karakter girebilirsin bro"),
                                                    Required(ErrorMessage ="Role Seç Bro")]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult ıdentityResult = await _roleManager.CreateAsync(new IdentityRole(name));

                if (ıdentityResult.Succeeded)
                {
                    TempData["Success"] = "The role has been created...!";
                    return RedirectToAction("Index");
                }
                else foreach (IdentityError error in ıdentityResult.Errors) ModelState.AddModelError("", error.Description);
            }
            TempData["Error"] = "The role hasn't been created..'";
            return View(name);
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();  // Rolleri atanan kullanıcılar listelenecek.
            List<AppUser> hasNotRole = new List<AppUser>(); // Rolleri atanmayan kullanıclar listenelecek.

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);
            }
            return View(new RoleEditDTOs
            {
                Role = role,
                HasRole = hasRole,
                HasNotRole = hasNotRole
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (RoleEditDTOs roleEditDTOs)
        {
            IdentityResult result;
            foreach (var userId in roleEditDTOs.AddIds)
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(appUser, roleEditDTOs.RoleName);
            }
            foreach (var userId in roleEditDTOs.DeleteIds)
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(appUser, roleEditDTOs.RoleName);
            }

            return RedirectToAction("Index");
        }

    }
}
