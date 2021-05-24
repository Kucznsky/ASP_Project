using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Link_Recipes_RecipeId",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId1",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "AppUserId1",
                table: "Recipes",
                newName: "UserRecipes");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Recipes",
                newName: "UserFavourites");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_AppUserId1",
                table: "Recipes",
                newName: "IX_Recipes_UserRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_AppUserId",
                table: "Recipes",
                newName: "IX_Recipes_UserFavourites");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Link",
                newName: "GaleryLinks");

            migrationBuilder.RenameIndex(
                name: "IX_Link_RecipeId",
                table: "Link",
                newName: "IX_Link_GaleryLinks");

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Recipes_GaleryLinks",
                table: "Link",
                column: "GaleryLinks",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserFavourites",
                table: "Recipes",
                column: "UserFavourites",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserRecipes",
                table: "Recipes",
                column: "UserRecipes",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Link_Recipes_GaleryLinks",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserFavourites",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserRecipes",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "UserRecipes",
                table: "Recipes",
                newName: "AppUserId1");

            migrationBuilder.RenameColumn(
                name: "UserFavourites",
                table: "Recipes",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserRecipes",
                table: "Recipes",
                newName: "IX_Recipes_AppUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserFavourites",
                table: "Recipes",
                newName: "IX_Recipes_AppUserId");

            migrationBuilder.RenameColumn(
                name: "GaleryLinks",
                table: "Link",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Link_GaleryLinks",
                table: "Link",
                newName: "IX_Link_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Recipes_RecipeId",
                table: "Link",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId",
                table: "Recipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId1",
                table: "Recipes",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
