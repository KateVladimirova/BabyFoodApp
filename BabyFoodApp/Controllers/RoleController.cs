using BabyFoodApp.BabyFoodCommons;
using BabyFoodApp.Data.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BabyFoodApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public RoleController(RoleManager<IdentityRole> _roleManager,
            UserManager<User> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {

            if (await roleManager.RoleExistsAsync(role.Name))
            {
                return View("Index", "Role");
            }

            role = new IdentityRole { Name = role.Name };
            
            await roleManager.CreateAsync(role);

            return RedirectToAction("Index");
        }
    }
}
