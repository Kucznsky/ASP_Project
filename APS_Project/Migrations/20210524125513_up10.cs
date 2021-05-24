using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId1",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RecipeId1",
                table: "AspNetUsers",
                newName: "Upvoters");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "AspNetUsers",
                newName: "Downvoters");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RecipeId1",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Upvoters");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RecipeId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Downvoters");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Recipes_Downvoters",
                table: "AspNetUsers",
                column: "Downvoters",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Recipes_Upvoters",
                table: "AspNetUsers",
                column: "Upvoters",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_Downvoters",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_Upvoters",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Upvoters",
                table: "AspNetUsers",
                newName: "RecipeId1");

            migrationBuilder.RenameColumn(
                name: "Downvoters",
                table: "AspNetUsers",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Upvoters",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RecipeId1");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Downvoters",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId",
                table: "AspNetUsers",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId1",
                table: "AspNetUsers",
                column: "RecipeId1",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
