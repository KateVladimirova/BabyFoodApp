using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabyFoodApp.Migrations
{
    public partial class ArticleTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Articles");
        }
    }
}
