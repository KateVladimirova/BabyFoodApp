using BabyFoodApp.Data;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BabyFoodApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public readonly ApplicationDbContext data;

        public UserController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            ApplicationDbContext _data)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            data = _data;
        }

        [HttpGet]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterViewModel();

            return View(model);
            
            //return View(); this is mine...upper code => Stamo
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

            var user = new User
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

                return View(model);
            }

            var logUser = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            
                    
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);

            //var result = new LoginViewModel();

            //if (User.Identity.IsAuthenticated == false)
            //{
            //    return RedirectToAction("Index", "Home");
            //}           

            //return View(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityUser user = await userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string Id)
        {
           var userToDelete = await userManager.FindByIdAsync(Id);

            if (userToDelete != null)
            {
                IdentityResult result = await userManager.DeleteAsync(userToDelete);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", "");
                }
            }

            return View(Id);
        }

        public async Task<string> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await userManager.FindByIdAsync(userId);
            var currentUser = data.Users.FirstOrDefault(x => x.Id == userId);

            return userId;
        }





        //public async Task<int> GetCurrentUser(string Id)
        //{
        //    //var user = await data.UserClaims.FirstAsync(c => c.UserId == userId);

        //    //return user;

        //    var user = await userManager.FindByIdAsync(Id);

        //    return user.Id;
        //}

    }
}
