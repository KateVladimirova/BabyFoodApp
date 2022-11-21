using BabyFoodApp.Areas.Identity.Pages.Account;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BabyFoodApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityUser user = new IdentityUser
            { 
                UserName = model.Email,
                Email = model.Email 
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                //return RedirectToAction(nameof(HomeController.Index), "Home");

                return View(model);
            }

            //return RedirectToAction(nameof(HomeController.Privacy), "Home");
            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}
