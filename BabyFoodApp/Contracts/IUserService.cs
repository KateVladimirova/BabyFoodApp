using BabyFoodApp.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace BabyFoodApp.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();

        UserDetailsModel UserDetails(string userId);
        Task<bool> DeteleUser(string userId);
    }
}
