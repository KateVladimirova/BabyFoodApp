using BabyFoodApp.BabyFoodCommons;
using BabyFoodApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models.Recipe
{
    public class DetailsRecipeViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.RecipeNameMaxLenght,
            MinimumLength = Constants.RecipeNameMinLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(Constants.RecipeDescriptionMaxLenght,
            MinimumLength = Constants.RecipeDescriptionMinLenght)]
        public string Description { get; set; } = null!;

        [Required]
        public int CookingTime { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public int TotalTime { get; set; }


    }
}
