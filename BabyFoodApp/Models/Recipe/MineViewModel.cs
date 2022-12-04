using BabyFoodApp.BabyFoodCommons;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models.Recipe
{
    public class MineViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.RecipeNameMaxLenght,
            MinimumLength = Constants.RecipeNameMinLenght)]
        public string Name { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;


    }
}
