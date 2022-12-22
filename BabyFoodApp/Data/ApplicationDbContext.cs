using BabyFoodApp.Data.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BabyFoodApp.Models.Recipe;
using Microsoft.AspNetCore.Identity;

namespace BabyFoodApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> //<User> in the Workshop files
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;

        public DbSet<Article> Articles { get; set; }

        //public DbSet<RecipeParts> RecipeParts { get; set; } = null!;
    }
}