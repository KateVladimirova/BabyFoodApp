using BabyFoodApp.BabyFoodCommons;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(Constants.EmailMaxLenght,
            MinimumLength = Constants.EmailMinLenght)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(Constants.PasswordMaxLenght,
            MinimumLength = Constants.PasswordMinLenght)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
