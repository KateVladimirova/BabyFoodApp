using BabyFoodApp.Data;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
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
        public readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AdministratorController(ApplicationDbContext _data,
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager)
        {
            data = _data;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        //public string Index() =>
        //"Administrator";

        [HttpGet]
        public async Task<ActionResult> GetAllRecipesById()
        {
            var recipes = await data.Recipes
               .Where(r => r.IsActive)
                .Select(r => new AllRecipesViewModel()
                {
                    Name = r.Name,
                    ImageUrl = r.ImageUrl,
                })
              .ToListAsync();

            return View(recipes);
        }

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
