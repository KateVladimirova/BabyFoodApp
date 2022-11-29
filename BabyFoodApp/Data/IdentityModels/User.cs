using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Data.IdentityModels
{
    public class User:IdentityUser
    {
        public override bool EmailConfirmed { get; set; } = false;
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
