using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Recipes_RecipeId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Recipes_RecipeId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Links",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Links",
                newName: "Link");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Links_RecipeId",
                table: "Link",
                newName: "IX_Link_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_RecipeId",
                table: "Category",
                newName: "IX_Category_RecipeId");

            migrationBuilder.AddColumn<int>(
                name: "RecipeOwnerId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Link",
                table: "Link",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeOwnerId",
                table: "Recipes",
                column: "RecipeOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Recipes_RecipeId",
                table: "Category",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Recipes_RecipeId",
                table: "Link",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_RecipeOwnerId",
                table: "Recipes",
                column: "RecipeOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Recipes_RecipeId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Link_Recipes_RecipeId",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_RecipeOwnerId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeOwnerId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Link",
                table: "Link");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "RecipeOwnerId",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Link",
                newName: "Links");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Link_RecipeId",
                table: "Links",
                newName: "IX_Links_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_RecipeId",
                table: "Categories",
                newName: "IX_Categories_RecipeId");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links",
                table: "Links",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Recipes_RecipeId",
                table: "Categories",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Recipes_RecipeId",
                table: "Links",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
