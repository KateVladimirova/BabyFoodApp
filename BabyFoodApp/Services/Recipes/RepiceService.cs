//using BabyFoodApp.Data;
//using BabyFoodApp.Services.Models;

//namespace BabyFoodApp.Services.Recipes
//{
//    public class RepiceService : IRecipeService
//    {
//        private readonly ApplicationDbContext data;
//        public RecipeService(ApplicationDbContext _data)
//        {
//            this.data = _data;
//        }
        

//        public IEnumerable<RecipeServiceModel> LastSixRecipes()
//        {
//            this.data.Recipes
//                .OrderByDescending(rp => rp.Id)
//                .Select(rp => new RecipeServiceModel
//                {
//                    Id = rp.Id,
//                    Name = rp.Name,
//                    ImageUrl = rp.ImageUrl
//                })
//                .Take(6);
//        }
//    }
//}
