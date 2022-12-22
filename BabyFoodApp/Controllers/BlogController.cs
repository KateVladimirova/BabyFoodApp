using BabyFoodApp.Contracts;
using BabyFoodApp.Data;
using BabyFoodApp.Models.Blog;
using BabyFoodApp.Models.Recipe;
using BabyFoodApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BabyFoodApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IBlogService blogService;

        public BlogController(ApplicationDbContext _data,
            IBlogService _blogService)
        {
            data = _data;
            blogService = _blogService;

        }
        public async Task<IActionResult> AllArticles(string? role)
        {
            var result = await blogService.All(role);
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {

            if (await blogService.Exists(id) == false)
            {
                return RedirectToAction(nameof(AllArticles));
            }

            var recipeDetails = await blogService.ArticleDetailsById(id);

            return View(recipeDetails);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleViewModel>>> AllArticlesByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await blogService.AllArticlesByUserId(userId);

            return View(result);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArticleViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await blogService.Create(model, userId);

            return RedirectToAction("AllArticlesByUserId", "Blog");

        }


        public async Task<IActionResult> Edit(int id)
        {
            var article = await blogService.ArticleDetailsById(id);

            return View(article);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticleViewModel model)
        {
            if (await blogService.Exists(id) == null)
            {
                return RedirectToAction(nameof(AllArticlesByUserId));
            }

            await blogService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { model.Id });

        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            blogService.Delete(id);

            return RedirectToAction(nameof(AllArticlesByUserId));

        }
    }
}
