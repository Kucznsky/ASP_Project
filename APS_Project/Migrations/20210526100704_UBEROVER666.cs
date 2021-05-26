using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class UBEROVER666 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipe_Recipes_CategoryId",
                table: "CategoryRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRecipe_RecipeId",
                table: "CategoryRecipe",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipe_Recipes_RecipeId",
                table: "CategoryRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecipe_Recipes_RecipeId",
                table: "CategoryRecipe");

            migrationBuilder.DropIndex(
                name: "IX_CategoryRecipe_RecipeId",
                table: "CategoryRecipe");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecipe_Recipes_CategoryId",
                table: "CategoryRecipe",
                column: "CategoryId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
