using BabyFoodApp.Data;
using BabyFoodApp.Data.Enums;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models;
using BabyFoodApp.Models.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BabyFoodApp.Controllers
{
    public class RecipeController : Controller
    {
        public readonly ApplicationDbContext data;
        public readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public RecipeController(ApplicationDbContext _data,
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager)
        {
            data = _data;
            userManager = _userManager;
            signInManager = _signInManager;
        }


        // GET: RecipeController
        public ActionResult Index()
        {
            return View();
        }

        //POST: RecipeController/All
        [HttpGet]        
        public async Task<ActionResult> All()
        {
            var recipes = await data.Recipes
                .Where(r => r.IsActive)
                 .Select(r => new AllRecipesViewModel()
                 {
                     Name = r.Name,
                     ImageUrl = r.ImageUrl,
                 })
               .ToListAsync(); ;

            return View(recipes);
        }

        //GET: RecipeController/MyRecipes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Mine()
        {
            var logedInUser = User?.Identity?.Name;
            var userName = await userManager.FindByNameAsync(logedInUser);

            var r = await data.Recipes
               .Where(r => r.UserId == userName.Id.ToString()
                      && r.IsActive == true)
               .Select(r => new MineViewModel()
               {
                   Name = r.Name,
                   ImageUrl = r.ImageUrl,
               })
               .ToListAsync();

            return View(r);
        }

        // GET: RecipeController/Add
        [HttpGet]
        public ActionResult Add()
        {
            //var model = new AddViewModel();

            return View();
        }

        //POST: RecipeController/Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddViewModel model) //, string userId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var logedInUser = User?.Identity?.Name;
            var userId = await userManager.FindByNameAsync(logedInUser);


            var recipe = new Recipe()
            {
                Name = model.Name,
                CookingTime = model.CookingTime,
                PreparationTime = model.PreparationTime,
                TotalTime = model.TotalTime,
                ChildAge = model.ChildAge,
                Category = model.Category,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                UserId = userId.Id
            };

            await data.AddAsync(recipe);
            await data.SaveChangesAsync();

            return RedirectToAction("Mine", "Recipe");
            //return RedirectToAction("Details", recipe.Id); //To see how to find the recipe
        }

        // GET: RecipeController/Details/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecipeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DetailsRecipeViewModel model) //new DTO for this method?
        {
            try
            {
                return RedirectToAction(nameof(Details), new { id = 1 });
            }
            catch
            {
                return View();
            }
        }


        // POST: RecipeController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int recipeId)
        {
            var recipe = await data.Recipes
                .FindAsync(recipeId);

            if(recipe == null)
            {
                recipe.IsActive = false;
            }


            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }


        public async Task<IEnumerable<Age>> AgeFilter()
        {
            return await data.Recipes
                .OrderBy(c => c.ChildAge)
                .Select(c => c.ChildAge)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> CategoryFilter()
        {
            return await data.Recipes
                .OrderBy(c => c.Category)
                .Select(c => c.Category)
                .ToListAsync();
        }
    }
}
