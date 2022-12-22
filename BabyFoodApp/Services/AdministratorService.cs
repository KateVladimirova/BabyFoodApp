using BabyFoodApp.Contracts;
using BabyFoodApp.Data;
using BabyFoodApp.Data.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace BabyFoodApp.Services
{
    public class AdministratorService :IAdministratorService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<User> _usermanager;

        public AdministratorService(ApplicationDbContext _data)
        {
            this.data = _data;
        }
        public void ChangeStatus(int recipeId, bool status)
        {
            var recipe = data.Recipes.FirstOrDefault(r => r.Id == recipeId);

            if (recipe != null)
            {
                if(recipe.IsActive == true)
                {
                    recipe.IsActive = false;
                }
                else
                {
                    recipe.IsActive = true;
                }
                data.SaveChanges();
            }
        }
        public void DeleteUser(string userId)
        {
            var user = data.Users.Find(userId);
            data.Users.Remove(user);

            data.SaveChanges();
        }
    }
}
