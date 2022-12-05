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
using System.Net;
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
                   Id = r.Id,
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
                UserId = userId.Id,
                IsActive = true
            };

            await data.AddAsync(recipe);
            await data.SaveChangesAsync();

            return RedirectToAction("Mine", "Recipe");
            //return RedirectToAction("Details", recipe.Id); //To see how to find the recipe
        }
            
              
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var recipe = data.Recipes
                .FirstOrDefault(r => r.Id == id && r.IsActive == true);

            var recipeDetails = new DetailsRecipeViewModel();

            if (recipe != null)
            {
                recipeDetails = new DetailsRecipeViewModel()
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    CookingTime = recipe.CookingTime,
                    PreparationTime = recipe.PreparationTime,
                    TotalTime = recipe.TotalTime,
                    ImageUrl = recipe.ImageUrl
                };
            }
            return View(recipeDetails);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

            var recipe = data.Recipes.Where(r => r.Id == id).FirstOrDefault();

            var recipeToEdit = new DetailsRecipeViewModel()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                CookingTime = recipe.CookingTime,
                PreparationTime = recipe.PreparationTime,
                TotalTime = recipe.TotalTime,
                ImageUrl = recipe.ImageUrl
            };

            return View(recipeToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DetailsRecipeViewModel model)
        {
            var recipe = await data.Recipes.FindAsync(id);


            if (recipe != null)
            {
                recipe.Name = model.Name;
                recipe.Description = model.Description;
                recipe.CookingTime = model.CookingTime;
                recipe.PreparationTime = model.PreparationTime;
                recipe.TotalTime = model.TotalTime;
                recipe.ImageUrl = model.ImageUrl;

                await data.SaveChangesAsync();
            };

            return RedirectToAction(nameof(Details), new { id = id});

        }

        
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Delete(int recipeId)
        //{
        //    var recipe = await data.Recipes
        //        .FindAsync(recipeId);


        //    if(recipe != null && recipe.IsActive == true)
        //    {
        //        data.Recipes.Remove(recipe);

        //        //recipe.IsActive = false;

        //        await data.SaveChangesAsync();


        //        return RedirectToAction(nameof(Mine));
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //    return RedirectToAction(nameof(Mine));
        //}

        [HttpPost]
        public IActionResult Delete(int id, DetailsRecipeViewModel model)
        {
            var r = data.Recipes.Find(id);

            if (r == null)
            {
                return NotFound();
            }

            r.IsActive = false;
            data.SaveChanges();

            return RedirectToAction(nameof(Mine));
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
