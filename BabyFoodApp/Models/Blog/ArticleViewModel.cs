using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BabyFoodApp.Data.IdentityModels;


namespace BabyFoodApp.Models.Blog
{
    public class ArticleViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public string? CreationDate { get; set; }

        public bool isActive { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

    }
}
