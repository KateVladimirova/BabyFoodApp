using BabyFoodApp.Contracts;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
using BabyFoodApp.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BabyFoodApp.Data;

namespace BabyFoodApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext data;

        public RecipeService(ApplicationDbContext _data)
        {
            this.data = _data;
        }

        //To check if works

        public async Task<IEnumerable<AllRecipesViewModel>> All()
        {
            return await data.Recipes
                .Where(r => r.IsActive)
                 .Select(r => new AllRecipesViewModel()
                 {
                     Name = r.Name,
                     ImageUrl = r.ImageUrl,
                 })
               .ToListAsync();
        }
        public async Task<int> Create(AddViewModel model, string userId)
        {

            Recipe recipe = new Recipe()
            {
                Name = model.Name,
                CookingTime = model.CookingTime,
                PreparationTime = model.PreparationTime,
                TotalTime = model.TotalTime,
                ChildAge = model.ChildAge,
                Category = model.Category,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                UserId = userId,
                Ingredients = model.Ingredients,
                IsActive = true
            };

            await data.AddAsync(recipe);
            await data.SaveChangesAsync();

            return recipe.Id;
        }

        public void  Delete(int recipeId)
        {
            var recipe =  data.Recipes.FirstOrDefault(r => r.Id == recipeId);
            
            if(recipe != null)
            {
                recipe.IsActive = false;
                data.SaveChanges();
            }            
        }

        public async Task Edit(int id, DetailsRecipeViewModel model)
        {
            var recipe = await data.Recipes.FirstOrDefaultAsync(r =>r.Id == id);

                recipe.Name = model.Name;
                recipe.Description = model.Description;
                recipe.CookingTime = model.CookingTime;
                recipe.PreparationTime = model.PreparationTime;
                recipe.TotalTime = model.TotalTime;
                recipe.ImageUrl = model.ImageUrl;

                await data.SaveChangesAsync();            
        }

        public async Task<DetailsRecipeViewModel> DetailsRecipeById(int id)
        {
            var recipe = new DetailsRecipeViewModel();


            recipe = await data.Recipes
                .Where(r => r.IsActive == true && r.Id == id)
                .Select(r => new DetailsRecipeViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    CookingTime = r.CookingTime,
                    PreparationTime = r.PreparationTime,
                    TotalTime = r.TotalTime,
                    Ingredients = r.Ingredients,
                    ImageUrl = r.ImageUrl
                })
                .FirstAsync();

            return recipe;
        }
        public async Task<bool> Exists(int id)
        {
            return await data.Recipes
                .AnyAsync(r => r.Id == id && r.IsActive);
        }

        public async Task<IEnumerable<MineViewModel>> AllRecipesByUserId(string userId)
        {
            return await data.Recipes
                .Where(r => r.UserId == userId && r.IsActive == true)
                   .Select(r => new MineViewModel()
                   {
                       Id = r.Id,
                       Name = r.Name,
                       ImageUrl = r.ImageUrl,
                   })
                   .ToListAsync();
        }
    }
}
