using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeOwnerId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeOwnerId",
                table: "Recipes");
        }
    }
}
