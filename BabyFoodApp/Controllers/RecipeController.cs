using BabyFoodApp.Models.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyFoodApp.Controllers
{
    public class RecipeController : Controller
    {
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
        [ValidateAntiForgeryToken]
        public ActionResult MyRecipes()
        {
            var model = new AddViewModel();

            return View(model);
        }

        // POST: RecipeController/MyRecipes
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MyRecipes(IFormCollection collection)
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
        public async Task<ActionResult> Add(AddViewModel collection)
        {
            try
            {
                return RedirectToAction("Details", new { id = 1 });
            }
            catch
            {
                return View();
            }
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
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: RecipeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecipeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
