using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRelations",
                table: "CategoryRelations");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RecipeIngredients");

            migrationBuilder.RenameTable(
                name: "CategoryRelations",
                newName: "CategoryRecipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe",
                columns: new[] { "CategoryId", "RecipeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecipe",
                table: "CategoryRecipe");

            migrationBuilder.RenameTable(
                name: "CategoryRecipe",
                newName: "CategoryRelations");

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "RecipeIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRelations",
                table: "CategoryRelations",
                columns: new[] { "CategoryId", "RecipeId" });
        }
    }
}
