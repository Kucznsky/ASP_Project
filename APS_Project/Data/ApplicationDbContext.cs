using APS_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public DbSet<CategoryRecipe> CategoryRecipe { get; set; }
        public DbSet<Category> Catergories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RecipeLink> RecipeLinks { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeVoter> RecipeVoters { get; set; }
        public DbSet<UserFavourite> UserFavourites { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryRecipe>()
                .HasKey(o => new { o.CategoryId, o.RecipeId });
            modelBuilder.Entity<RecipeVoter>()
                .HasKey(o => new { o.RecipeId, o.VoterId });
            modelBuilder.Entity<UserFavourite>()
                .HasKey(o => new { o.RecipeId, o.UserId });
        }

    }
}
