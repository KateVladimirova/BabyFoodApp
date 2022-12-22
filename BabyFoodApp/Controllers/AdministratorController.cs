using BabyFoodApp.Contracts;
using BabyFoodApp.Data;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
using BabyFoodApp.Models.User;
using BabyFoodApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BabyFoodApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        public readonly ApplicationDbContext data;
        public readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService users;
        private readonly IAdministratorService admin;
        private readonly IRecipeService recipeService;


        public AdministratorController(ApplicationDbContext _data,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IUserService _users,
            IAdministratorService _admin,
            IRecipeService _recipeService)
        {
            data = _data;
            userManager = _userManager;
            signInManager = _signInManager;
            users = _users;
            admin = _admin;
            recipeService = _recipeService;
        }

        //public string Index() =>
        //"Administrator";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await userManager.FindByIdAsync(currentUserId);
            var roles = await userManager.GetRolesAsync(currentUser);

            var recipes = await recipeService.All(roles.First());

            return View(recipes);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsers()
        {
            var usersList = await users.GetAllUsers();

            return View(usersList);

        }

        [HttpGet]
        public IActionResult UserDetails(UserViewModel data)
        {
            var user = users.UserDetails(data.Id);

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, bool status)
        {
            admin.ChangeStatus(id, status);
            return RedirectToAction(nameof(UserDetails), new { Id=User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }

        [HttpPost]
        public IActionResult ChangeRecipeStatus(int id, bool status)
        {
            admin.ChangeStatus(id, status);
            return RedirectToAction(nameof(GetAllRecipes));
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            admin.DeleteUser(id);
            return RedirectToAction(nameof(GetAllUsers));
        }


    }
}
