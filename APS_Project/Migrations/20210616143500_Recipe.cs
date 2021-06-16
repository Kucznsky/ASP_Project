using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class Recipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDislikeRecipes_AspNetUsers_AppUserId",
                table: "UserDislikeRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDislikeRecipes_Recipes_RecipeId",
                table: "UserDislikeRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowRecipes_AspNetUsers_AppUserId",
                table: "UserFollowRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowRecipes_Recipes_RecipeId",
                table: "UserFollowRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikeRecipes_AspNetUsers_AppUserId",
                table: "UserLikeRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikeRecipes_Recipes_RecipeId",
                table: "UserLikeRecipes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDislikeRecipes_AspNetUsers_AppUserId",
                table: "UserDislikeRecipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDislikeRecipes_Recipes_RecipeId",
                table: "UserDislikeRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowRecipes_AspNetUsers_AppUserId",
                table: "UserFollowRecipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowRecipes_Recipes_RecipeId",
                table: "UserFollowRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikeRecipes_AspNetUsers_AppUserId",
                table: "UserLikeRecipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikeRecipes_Recipes_RecipeId",
                table: "UserLikeRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDislikeRecipes_AspNetUsers_AppUserId",
                table: "UserDislikeRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDislikeRecipes_Recipes_RecipeId",
                table: "UserDislikeRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowRecipes_AspNetUsers_AppUserId",
                table: "UserFollowRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowRecipes_Recipes_RecipeId",
                table: "UserFollowRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikeRecipes_AspNetUsers_AppUserId",
                table: "UserLikeRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikeRecipes_Recipes_RecipeId",
                table: "UserLikeRecipes");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDislikeRecipes_AspNetUsers_AppUserId",
                table: "UserDislikeRecipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDislikeRecipes_Recipes_RecipeId",
                table: "UserDislikeRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowRecipes_AspNetUsers_AppUserId",
                table: "UserFollowRecipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowRecipes_Recipes_RecipeId",
                table: "UserFollowRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikeRecipes_AspNetUsers_AppUserId",
                table: "UserLikeRecipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikeRecipes_Recipes_RecipeId",
                table: "UserLikeRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }
    }
}
