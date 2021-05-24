using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_RecipeOwnerId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeOwnerId",
                table: "Recipes",
                newName: "Owner");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeOwnerId",
                table: "Recipes",
                newName: "IX_Recipes_Owner");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_Owner",
                table: "Recipes",
                column: "Owner",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_Owner",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Recipes",
                newName: "RecipeOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_Owner",
                table: "Recipes",
                newName: "IX_Recipes_RecipeOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_RecipeOwnerId",
                table: "Recipes",
                column: "RecipeOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
