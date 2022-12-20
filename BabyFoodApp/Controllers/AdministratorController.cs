using BabyFoodApp.Contracts;
using BabyFoodApp.Data;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
using BabyFoodApp.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabyFoodApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        public readonly ApplicationDbContext data;
        public readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService users;
        public AdministratorController(ApplicationDbContext _data,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IUserService _users)
        {
            data = _data;
            userManager = _userManager;
            signInManager = _signInManager;
            users = _users;

        }

        //public string Index() =>
        //"Administrator";

        public IActionResult Index()
        {
            return View();
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
        //[HttpGet]
        //public async Task<ActionResult> GetAllRecipesById()
        //{
        //    var recipes = await data.Recipes
        //       .Where(r => r.IsActive)
        //        .Select(r => new AllRecipesViewModel()
        //        {
        //            Name = r.Name,
        //            ImageUrl = r.ImageUrl,
        //        })
        //      .ToListAsync();

        //    return View(recipes);
        //}

        //[HttpGet]
        //public async Task<ActionResult> GetAllUsersById()
        //{
        //    var recipes = await data.Users
        //       .FindAsync(User)
        //        .Select(r => new AllRecipesViewModel()
        //        {

        //        })
        //      .ToListAsync();

        //    return View(recipes);
        //}

    }
}
