namespace BabyFoodApp.Contracts
{
    public interface IAdministratorService
    {
        void ChangeStatus(int recipeId, bool status);
        void DeleteUser(string userId);

    }
}
