using BabyFoodApp.BabyFoodCommons;
using BabyFoodApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models.Recipe
{
    public class DetailsRecipeViewModel
    {
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
        [Display(Name = "Cooking Time")]
        public int CookingTime { get; set; }

        [Required]
        [Display(Name = "Preparation Time")]
        public int PreparationTime { get; set; }

        [Required]
        [Display(Name = "Total Time")]
        public int TotalTime { get; set; }

        [Required]
        [Display(Name = "Child Age")]
        public Age ChildAge { get; set; }

        [Required]
        public Category Category { get; set; }


        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }
    }
}
