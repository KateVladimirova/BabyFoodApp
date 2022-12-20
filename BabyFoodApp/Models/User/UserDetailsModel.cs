using BabyFoodApp.Models.Recipe;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models.User
{
    public class UserDetailsModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public ICollection<AllRecipesViewModel> UserRecipes { get; set; } = new List<AllRecipesViewModel>();
    }
}
