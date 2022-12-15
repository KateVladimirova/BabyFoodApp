namespace BabyFoodApp.Models.Recipe
{
    public class RecipeQueryModel
    {
        public int TotalRecipesCount { get; set; }

        public IEnumerable<AllRecipesViewModel> Recipes { get; set; } = new List<AllRecipesViewModel>();
    }
}
