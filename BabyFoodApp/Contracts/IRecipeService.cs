using BabyFoodApp.Models.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace BabyFoodApp.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<AllRecipesViewModel>> All();
        Task<int> Create(AddViewModel model, string userId);
        void Delete(int recipeId);
        Task Edit(int id, DetailsRecipeViewModel model);
        Task<bool> Exists(int id);

        Task<DetailsRecipeViewModel> DetailsRecipeById(int id);

        Task<IEnumerable<MineViewModel>> AllRecipesByUserId(string userId);
    }
}
