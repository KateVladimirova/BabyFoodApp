using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BabyFoodApp.Data.Enums
{
    public enum Age
    {
        [Display(Name = "Baby")]
        Baby,
        [Display(Name = "Toddler")]
        Toddler,
        [Display(Name = "Teenage")]
        Teenage
    }
}
