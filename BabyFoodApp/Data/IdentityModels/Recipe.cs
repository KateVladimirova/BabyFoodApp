using BabyFoodApp.BabyFoodCommons;
using BabyFoodApp.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyFoodApp.Data.IdentityModels
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.RecipeNameMaxLenght,
            MinimumLength = Constants.RecipeNameMinLenght)]
        public string Name { get; set; } = null!;
               
        [Required]
        public int CookingTime { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public int TotalTime { get; set; }

        [Required]
        public Age ChildAge { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(Constants.RecipeDescriptionMaxLenght,
           MinimumLength = Constants.RecipeDescriptionMinLenght)]
        public string Description { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; } = true;

        //public ICollection<RecipeParts> RecipeParts { get; set; } = new List<RecipeParts>();
    }

}
