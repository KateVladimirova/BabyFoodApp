namespace BabyFoodApp.BabyFoodCommons
{
    public  class Constants
    {
        //Recipe

        public  const int RecipeNameMaxLenght = 50;
        public const int RecipeNameMinLenght = 2;

        public const int RecipeDescriptionMaxLenght = int.MaxValue;
        public const int RecipeDescriptionMinLenght = 100;

        //User

        public const int UserEmailMaxLenght = 320;
        public const int UserEmailMinLenght = 4;

        public const int UserPasswordMaxLenght = 20;
        public const int UserPasswordMinLenght = 8;

        //AdminConstants
        public const string AdminRoleName = "Administrator";
        public const string AdminEmail = "admin@babyfood.com";
    }
}
