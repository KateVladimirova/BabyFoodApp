using BabyFoodApp.Contracts;
using BabyFoodApp.Data;
using BabyFoodApp.Data.Common;
using BabyFoodApp.Data.Enums;
using BabyFoodApp.Data.IdentityModels;
using BabyFoodApp.Models.Recipe;
using BabyFoodApp.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BabyFoodApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        private readonly UserManager<User> userManager;

        public UserService(
            ApplicationDbContext _data,
            UserManager<User> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {            
            return await data.Users
                .Where( u=> u.Id != null && u.Id != string.Empty)
                .Select(u => new UserViewModel()
                {
                    Id= u.Id,  
                    Email = u.Email
                })
                .ToListAsync();
        }

        public Task<bool> DeteleUser(string userId)
        {
            throw new NotImplementedException();
        }

        public UserDetailsModel UserDetails(string userId)
        {
            var user = data.Users.FirstOrDefault(u => u.Id == userId);

            var recipeByUser = data.Recipes
                .Where(r => r.UserId == user.Id)
                 .Select(r => new AllRecipesViewModel()
                 {
                     Id = r.Id,
                     Name = r.Name,
                     IsActive = r.IsActive,
                     ImageUrl = r.ImageUrl
                 })
               .ToList();

            var userDetails = new UserDetailsModel()
            {
                Id = userId,
                Email = user!.Email,
                UserRecipes = new List<AllRecipesViewModel>(recipeByUser)


            };

            return userDetails;
        }
    }
}
