using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    public class IndexModel : PageModel
    {
        const int Quantity = 10;
        //private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        public List<Recipe> Recipes { get; set; }


        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
        {
            
            // _logger = logger;
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            //tym dodajecie jedna recipe
            /*_dbContext.Recipes.Add(new Recipe()
            {
                Title = "Best burgi",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ImageName = "burger.jpg",
            });
            await _dbContext.SaveChangesAsync();
            */
            Recipes = await _dbContext.Recipes.Take(Quantity).ToListAsync();
            foreach (var recipe in Recipes)
            {
                recipe.Likes = _dbContext.RecipeVoters
                    .Where(p => p.RecipeId == recipe.RecipeId && p.Vote == true).Count()
                    - _dbContext.RecipeVoters
                    .Where(p => p.RecipeId == recipe.RecipeId && p.Vote == false).Count();
                recipe.IsUserFavourite = _dbContext.UserFavourites.Find(recipe.RecipeId, 0) is not null;
            }
        }
        public async Task<IActionResult> OnPostAsync(int recipeId, int userId, bool like)
        {
            var vote = _dbContext.RecipeVoters.Find(recipeId, userId);
            if (vote is not null)
            {
                if (vote.Vote == like)
                {
                    _dbContext.RecipeVoters.Remove(vote);
                }
                else
                    vote.Vote = like;
            }
            else
                await _dbContext.RecipeVoters.AddAsync(new RecipeVoter() { RecipeId = recipeId, VoterId = userId, Vote = like });
            await _dbContext.SaveChangesAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostFavouritesAsync(int recipeId, int userId)
        {
            var follows = _dbContext.UserFavourites.Find(recipeId, userId);
            if (follows is not null)
            {
                _dbContext.UserFavourites.Remove(follows);
            }
            else
            {
                await _dbContext.UserFavourites.AddAsync(new UserFavourite() { RecipeId = recipeId, UserId = userId });
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
