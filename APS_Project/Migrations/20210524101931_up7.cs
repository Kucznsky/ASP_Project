using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRecipe");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeLinks");

            migrationBuilder.DropTable(
                name: "RecipeVoters");

            migrationBuilder.DropTable(
                name: "UserFavourites");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId1",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Indigrients",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkToImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AppUserId1",
                table: "Recipes",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RecipeId",
                table: "AspNetUsers",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RecipeId1",
                table: "AspNetUsers",
                column: "RecipeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Links_RecipeId",
                table: "Links",
                column: "RecipeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId1",
                table: "Recipes",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Recipes_RecipeId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId1",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AppUserId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RecipeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RecipeId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Indigrients",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RecipeId1",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.CreateTable(
                name: "CategoryRecipe",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRecipe", x => new { x.CategoryId, x.RecipeId });
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ingredient = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeLinks",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeLinks", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeVoters",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeVoters", x => new { x.RecipeId, x.VoterId });
                });

            migrationBuilder.CreateTable(
                name: "UserFavourites",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavourites", x => new { x.RecipeId, x.UserId });
                });
        }
    }
}
