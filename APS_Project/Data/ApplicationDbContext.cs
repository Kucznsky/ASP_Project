using APS_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APS_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<CategoryRecipe> CategoryRecipe {get;set;}
        public DbSet<Category> Category { get; set; }
        public DbSet<UserDislikeRecipe> UserDislikeRecipes { get; set; }
        public DbSet<UserFollowRecipe> UserFollowRecipes { get; set; }
        public DbSet<UserLikeRecipe> UserLikeRecipes { get; set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Recipe>().HasKey(r => r.RecipeId);
            builder.Entity<AppUser>().HasKey(au => au.Id);
            builder.Entity<Link>().HasKey(l => l.Id);
            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<CategoryRecipe>().HasKey(cr => new {cr.CategoryId, cr.RecipeId });
            builder.Entity<UserDislikeRecipe>().HasKey(udr => new { udr.AppUserId, udr.RecipeId });
            builder.Entity<UserLikeRecipe>().HasKey(udr => new { udr.AppUserId, udr.RecipeId });
            builder.Entity<UserFollowRecipe>().HasKey(ufr => new { ufr.AppUserId, ufr.RecipeId });

            builder.Entity<UserDislikeRecipe>()
                .HasOne<AppUser>(udr => udr.AppUser)
                .WithMany(au => au.UserDislikes)
                .HasForeignKey(udr => udr.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserDislikeRecipe>()
                .HasOne<Recipe>(udr => udr.Recipe)
                .WithMany(r => r.RecipeDisliker)
                .HasForeignKey(udr => udr.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserLikeRecipe>()
                .HasOne<AppUser>(udr => udr.AppUser)
                .WithMany(au => au.UserLikes)
                .HasForeignKey(udr => udr.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserLikeRecipe>()
                .HasOne<Recipe>(udr => udr.Recipe)
                .WithMany(r => r.RecipeLiker)
                .HasForeignKey(udr => udr.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserFollowRecipe>()
                .HasOne<AppUser>(ufr => ufr.AppUser)
                .WithMany(au => au.UserFollows)
                .HasForeignKey(ufr => ufr.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserFollowRecipe>()
                .HasOne<Recipe>(ufr => ufr.Recipe)
                .WithMany(r => r.RecipeFollower)
                .HasForeignKey(ufr => ufr.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Recipe>()
                .HasMany<Link>(r => r.Links)
                .WithOne(l => l.Recpie)
                .HasForeignKey(r=>r.RecipeId);

            builder.Entity<CategoryRecipe>()
                .HasOne<Recipe>(cr => cr.Recipe)
                .WithMany(r => r.CategoryRecipe)
                .HasForeignKey(cr=>cr.RecipeId);
            builder.Entity<CategoryRecipe>()
                .HasOne<Category>(cr => cr.Category)
                .WithMany(c => c.CategoryRecipes)
                .HasForeignKey(cr => cr.CategoryId);

        }
    }
}
