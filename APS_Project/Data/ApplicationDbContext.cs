using APS_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CategoryRecipe> CategoryRelations { get; set; }
        public DbSet<Catergory> Catergories { get; set; }
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
                .HasKey(o => new { o.RecipeId, o.Vote });
            modelBuilder.Entity<UserFavourite>()
                .HasKey(o => new { o.RecipeId, o.UserId });
        }



        /// <param name="recipeId"></param>
        /// <returns>Quantity of likes of recipe with given Id</returns>
        public int RecipeLikes(int recipeId)
        {
            return RecipeVoters.Where(p => p.RecipeId == recipeId).Count()
                - RecipeVoters.Where(p => p.RecipeId == recipeId && p.Vote == false).Count();
        }

        /// <param name="recipeId"></param>
        /// <param name="userId"></param>
        /// <returns>true if this recipe is liked by this user</returns>
        public async Task<bool> IsRecipeUserFavourite(int recipeId, int userId)
        {
            return await UserFavourites.FindAsync(recipeId, userId) is not null;
        }
        /// <summary>
        /// Votes recipe(recipeId) by user(userId) with vote and sets recipe.RecipeLikes
        /// </summary>
        /// <param name="vote"></param>
        /// <param name="recipeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task VoteRecipe(bool vote, int recipeId, int userId)
        {
            var voteItem = await RecipeVoters.FindAsync(recipeId, userId);
            var recipe = await Recipes.FindAsync(recipeId);
            if (voteItem is not null)
            {
                voteItem.Vote = vote;
                recipe.RecipeLikes++;
            }
            else
            {
                await RecipeVoters.AddAsync(new RecipeVoter() { RecipeId = recipeId, Vote = vote, VoterId = userId });
                recipe.RecipeLikes--;
            }
            await SaveChangesAsync();
        }
        /// <param name="recipeId"></param>
        /// <param name="userId"></param>
        /// <returns>true/false if user(userId) already voted recipe(recipeId) or null</returns>
        public async Task<bool?> RecipeUserVote(int recipeId, int userId)
        {
            var item = await RecipeVoters.FindAsync(recipeId, userId);
            return item is not null ? item.Vote : null;
        }
    }
}
