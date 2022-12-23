using BabyFoodApp.Contracts;
using BabyFoodApp.Data;
using BabyFoodApp.Data.Enums;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> All(string? role)
        {
            var recipes = await recipeService.All(role);

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await data.Recipes
                .FindAsync(id);

            var model = new DetailsRecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                CookingTime = recipe.CookingTime,
                PreparationTime = recipe.PreparationTime,
                TotalTime = recipe.TotalTime,
                ImageUrl = recipe.ImageUrl,
                ChildAge = recipe.ChildAge,
                Category = recipe.Category
            };

            return View(model);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DetailsRecipeViewModel model)
        {
            if (await recipeService.Exists(id) == null)
            {
                return RedirectToAction(nameof(Mine));
            }


            await recipeService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { model.Id });

        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {

            recipeService.Delete(id);

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
