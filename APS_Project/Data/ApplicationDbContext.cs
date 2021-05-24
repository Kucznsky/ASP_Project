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
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().HasMany(user => user.UserFavourites);
            builder.Entity<AppUser>().HasMany(user => user.UserRecipes);
            builder.Entity<Recipe>().HasMany(recipe => recipe.Upvoters);
            builder.Entity<Recipe>().HasMany(recipe => recipe.Downvoters);
            builder.Entity<Recipe>().HasOne(recipe => recipe.RecipeOwner);
        }
    }
}
