using MessagePack.Formatters;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace BabyFoodApp.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
                
        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
