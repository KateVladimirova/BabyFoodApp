using Microsoft.AspNetCore.Identity;

namespace BabyFoodApp.Data.IdentityModels
{
    public class User : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; } = null!;
    }
}
