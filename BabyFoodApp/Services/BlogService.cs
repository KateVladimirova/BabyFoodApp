using BabyFoodApp.Contracts;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Data;
using BabyFoodApp.Models.Blog;
using Microsoft.AspNetCore.Identity;
using BabyFoodApp.Models.Recipe;
using Microsoft.EntityFrameworkCore;

namespace BabyFoodApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<User> userManager;

        public BlogService(ApplicationDbContext _data,
            UserManager<User> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }
        public async Task<IEnumerable<ArticleViewModel>> All(string? role)
        {
            return data.Articles
                .Where(a => a.isActive == true)
                .Select(a => new ArticleViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,

                }).ToList();

        }

        public async Task<IEnumerable<ArticleViewModel>> AllArticlesByUserId(string userId)
        {
            return data.Articles
               .Where(a => a.isActive == true
               && a.UserId == userId)
               .Select(a => new ArticleViewModel()
               {
                   Id = a.Id,
                   Title = a.Title,
                   Content = a.Content,

               }).ToList();
        }

        public async Task<ArticleViewModel> ArticleDetailsById(int id)
        {
            return await data.Articles
                .Where(a => a.isActive == true && a.Id == id)
                .Select(a => new ArticleViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    CreationDate = a.CreationDate,
                    UserId = a.UserId,
                })
                .FirstAsync();
        }

        public async Task<Article> Create(ArticleViewModel model, string userId)
        {
            Article article = new Article()
            {
                Title = model.Title,
                Content = model.Content,
                CreationDate = DateTime.Now.ToString("F"),
                UserId = userId
            };

            await data.AddAsync(article);
            await data.SaveChangesAsync();

            return article;
        }

        public void Delete(int articleId)
        {
            var article = data.Articles.FirstOrDefault(r => r.Id == articleId);

            if (article != null)
            {
                article.isActive = false;
                data.SaveChanges();
            }
        }

        public async Task Edit(int id, ArticleViewModel model)
        {
            var article = await data.Articles.FirstOrDefaultAsync(a => a.Id == id);

            article.Id = model.Id;
            article.Title = model.Title;
            article.Content = model.Content;
            article.CreationDate = model.CreationDate;

            await data.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await data.Articles
                 .AnyAsync(a => a.Id == id && a.isActive);
        }
    }
}
