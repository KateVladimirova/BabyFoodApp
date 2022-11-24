using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BabyFoodApp.Data.Enums
{
    public enum Category
    {
        [Display(Name = "Main")]
        Main,
        [Display(Name = "Snack")]
        Snack,
        [Display(Name = "Dinner")]
        Dinner,
        [Display(Name = "Dessert")]
        Dessert
    }
}
