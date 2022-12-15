using BabyFoodApp.Contracts;
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

        public readonly IRecipeService recipeService;
        public readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public RecipeController(IRecipeService _recipeService,
            ApplicationDbContext _data,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager)
        {
            recipeService = _recipeService;
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
        public async Task<IActionResult> All()
        {
            var recipes = await recipeService.All();

            return View(recipes);
        }

        //GET: RecipeController/MyRecipes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Mine()
        {
            IEnumerable<MineViewModel> myRecipes = new List<MineViewModel>();

            var logedInUser = User?.Identity?.Name;
            var userName = await userManager.FindByNameAsync(logedInUser);

            if (userName != null)
            {
                myRecipes = await recipeService.AllRecipesByUserId(userName.Id);
            }

            return View(myRecipes);
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
        public async Task<ActionResult> Add(AddViewModel model)
        {
            var logedInUser = User?.Identity?.Name;
            var user = await userManager.FindByNameAsync(logedInUser);

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            int id = await recipeService.Create(model, user.Id);

            return RedirectToAction("Mine", "Recipe");
            //return RedirectToAction("Details", recipe.Id); //To see how to find the recipe
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await recipeService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var recipeDetails = await recipeService.DetailsRecipeById(id);

            return View(recipeDetails);
        }

        //[HttpGet]
        //[Authorize]
        //public ActionResult Edit(int id)
        //{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }

        //    var recipe = data.Recipes.Where(r => r.Id == id).FirstOrDefault();

        //    var recipeToEdit = new DetailsRecipeViewModel()
        //    {
        //        Id = recipe.Id,
        //        Name = recipe.Name,
        //        Description = recipe.Description,
        //        CookingTime = recipe.CookingTime,
        //        PreparationTime = recipe.PreparationTime,
        //        TotalTime = recipe.TotalTime,
        //        ImageUrl = recipe.ImageUrl
        //    };

        //    return View(recipeToEdit);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, DetailsRecipeViewModel model)
        //{
        //    var recipe = await data.Recipes.FindAsync(id);


        //    if (recipe != null)
        //    {
        //        recipe.Name = model.Name;
        //        recipe.Description = model.Description;
        //        recipe.CookingTime = model.CookingTime;
        //        recipe.PreparationTime = model.PreparationTime;
        //        recipe.TotalTime = model.TotalTime;
        //        recipe.ImageUrl = model.ImageUrl;

        //        await data.SaveChangesAsync();
        //    };

        //    return RedirectToAction(nameof(Details), new { id = id});

        //}


        ////[HttpPost]
        ////[Authorize]
        ////public async Task<IActionResult> Delete(int recipeId)
        ////{
        ////    var recipe = await data.Recipes
        ////        .FindAsync(recipeId);


        ////    if(recipe != null && recipe.IsActive == true)
        ////    {
        ////        data.Recipes.Remove(recipe);

        ////        //recipe.IsActive = false;

        ////        await data.SaveChangesAsync();


        ////        return RedirectToAction(nameof(Mine));
        ////    }
        ////    else
        ////    {
        ////        return NotFound();
        ////    }

        ////    return RedirectToAction(nameof(Mine));
        ////}

        //[HttpPost]
        //public IActionResult Delete(int id, DetailsRecipeViewModel model)
        //{
        //    var r = data.Recipes.Find(id);

        //    if (r == null)
        //    {
        //        return NotFound();
        //    }

        //    r.IsActive = false;
        //    data.SaveChanges();

        //    return RedirectToAction(nameof(Mine));
        //}



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
