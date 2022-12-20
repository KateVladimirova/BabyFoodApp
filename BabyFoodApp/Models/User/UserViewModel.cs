using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models.User
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
