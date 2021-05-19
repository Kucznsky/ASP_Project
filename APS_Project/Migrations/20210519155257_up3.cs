using Microsoft.EntityFrameworkCore.Migrations;

namespace APS_Project.Migrations
{
    public partial class up3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeVoters",
                table: "RecipeVoters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeVoters",
                table: "RecipeVoters",
                columns: new[] { "RecipeId", "VoterId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeVoters",
                table: "RecipeVoters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeVoters",
                table: "RecipeVoters",
                columns: new[] { "RecipeId", "Vote" });
        }
    }
}
