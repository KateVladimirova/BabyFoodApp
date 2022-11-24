using BabyFoodApp.Data;
using BabyFoodApp.Data.Enums;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabyFoodApp.Controllers
{
    public class RecipeController : Controller
    {
        public readonly ApplicationDbContext data;

        public RecipeController(ApplicationDbContext _data)
        {
            data = _data;
        }


        // GET: RecipeController
        public ActionResult Index()
        {
            return View();
        }



        // GET: RecipeController/All
        public ActionResult All()
        {
            var model = new AllRecipesViewModel();

            return View(model);
        }

        // POST: RecipeController/All
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> All(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("All");
            }
        }

        // GET: RecipeController/MyRecipes

        [HttpGet]
        [Authorize]
        public ActionResult Mine()
        {
            var model = new MineViewModel();

            return View(model);
        }

        // POST: RecipeController/MyRecipes
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Mine(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeController/Add
        public ActionResult Add()
        {
            var model = new AddViewModel();

            return View(model);
        }

        //POST: RecipeController/Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddViewModel model)
        {
                var recipe = new Recipe()
                {
                    Name = model.Name,
                    CookingTime = model.CookingTime,
                    PreparationTime = model.PreparationTime,
                    TotalTime = model.TotalTime,
                    ChildAge = model.ChildAge,
                    Category = model.Category,
                    ImageUrl = model.ImageUrl,
                    Description = model.Description
                };

                await data.AddAsync(recipe);
                await data.SaveChangesAsync();

                return RedirectToAction("All", "Recipe");
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

        // GET: RecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new DetailsRecipeViewModel();

            return View(model);
        }

        // POST: RecipeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(All));
            }
            catch
            {
                return View();
            }
        }

        //public async Task<IEnumerable<Recipe>> AllRecipesByUserId(string userId)
        //{
        //    return await data.Recipes
        //        .Where(r => r.UserId == userId)
        //        .Where(r => r.IsActive)
        //        .Select(r => new Recipe()
        //        {
        //            Name = r.Name,
        //            CookingTime = r.CookingTime,
        //            PreparationTime = r.PreparationTime,
        //            TotalTime = r.TotalTime,
        //            ChildAge = r.ChildAge,
        //            Category = r.Category,
        //            ImageUrl = r.ImageUrl,
        //            Description = r.Description,
        //            UserId = userId
        //        })
        //        .ToListAsync();
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
