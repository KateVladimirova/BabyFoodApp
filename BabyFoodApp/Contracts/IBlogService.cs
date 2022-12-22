using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Blog;
using BabyFoodApp.Models.Recipe;

namespace BabyFoodApp.Contracts
{
    public interface IBlogService
    {
        Task<IEnumerable<ArticleViewModel>> All(string? role);
        Task<Article> Create(ArticleViewModel model, string userId);
        void Delete(int articleId);
        Task Edit(int id, ArticleViewModel model);
        Task<bool> Exists(int id);

        Task<ArticleViewModel> ArticleDetailsById(int id);

        Task<IEnumerable<ArticleViewModel>> AllArticlesByUserId(string userId);
    }
}
