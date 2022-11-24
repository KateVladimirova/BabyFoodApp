using BabyFoodApp.Data.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BabyFoodApp.Models.Recipe;

namespace BabyFoodApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;

        public DbSet<BabyFoodApp.Models.Recipe.DetailsRecipeViewModel> DetailsRecipeViewModel { get; set; }

        public DbSet<BabyFoodApp.Models.Recipe.AddViewModel> AddViewModel { get; set; }

      //  public DbSet<BabyFoodApp.Models.Recipe.AddViewModel> AddViewModel { get; set; }
      //
      //  public DbSet<BabyFoodApp.Models.Recipe.AllRecipesViewModel> AllRecipesViewModel { get; set; }
    }
}