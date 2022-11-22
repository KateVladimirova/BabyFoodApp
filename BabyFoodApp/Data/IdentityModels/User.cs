using Microsoft.AspNetCore.Identity;

namespace BabyFoodApp.Data.IdentityModels
{
    public class User : IdentityUser
    {
        public bool EmailConfirmed { get; set; } = false;
        public ICollection<Recipe> Recipes { get; set; } = null!;
    }
}
