using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    public class IndexModel : PageModel
    {
        const int Quantity = 10;
        //private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        public readonly int userId;
        public List<Recipe> Recipes { get; set; }
        public List<Recipe> Searched { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            // _logger = logger;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            Recipes = new List<Recipe>();
            Searched = new List<Recipe>();

        }
        public async Task<IActionResult> OnPostAddRecipeAsync() 
        {
            if (User.Identity.IsAuthenticated)
            {
                //tym dodajecie jedna recipe
                _dbContext.Recipes.Add(new Recipe()
                {
                    Title = "Best burgi",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    ImageName = "burger.jpg",
                    OwnerId = userId
                });
                await _dbContext.SaveChangesAsync();
                return RedirectToPage();
            }
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }
        public async Task OnGetAsync()
        {
            Recipes = await _dbContext.Recipes.Take(Quantity).ToListAsync();
            foreach (var recipe in Recipes)
            {
                recipe.IsUserFavourite = await _dbContext.UserFavourites.FindAsync(recipe.RecipeId, userId) is not null;
            }
        }
        public async Task<IActionResult> OnPostAsync(int recipeId, bool like)
        {
            if (User.Identity.IsAuthenticated)
            {
                var vote = await _dbContext.RecipeVoters.FindAsync(recipeId, userId);
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

                var recipe = await _dbContext.Recipes.FindAsync(recipeId);
                recipe.Likes = _dbContext.RecipeVoters
                    .Where(p => p.RecipeId == recipe.RecipeId && p.Vote == true).Count()
                    - _dbContext.RecipeVoters
                    .Where(p => p.RecipeId == recipe.RecipeId && p.Vote == false).Count();

                await _dbContext.SaveChangesAsync();
                return RedirectToPage();
            }
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }
        public async Task<IActionResult> OnPostFavouritesAsync(int recipeId)
        {
            if (User.Identity.IsAuthenticated)
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
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }

        public async Task<IActionResult> OnPostSearchAsync(string title, string author, string category)
        {
            Searched = await _dbContext.Recipes.ToListAsync();
            if (!string.IsNullOrEmpty(title))
            {
                Searched = Searched.Where(p => p.Title == title).ToList();
            }
            if (!string.IsNullOrEmpty(author))
            {
                var Author = await _dbContext.AppUsers.Where(p => p.Name == author).ToListAsync();
                Searched = Searched.Where(p => Author.Any(c => c.Id == p.OwnerId)).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                var Category = await _dbContext.Categories
                    .Where(p => p.Name == category)
                    .ToListAsync();
                var CategoryRecipe = await _dbContext.CategoryRecipe
                    .Where(p => Category.Any(c => c.CategoryID == p.CategoryId))
                    .ToListAsync();
                Searched = Searched
                    .Where(p => CategoryRecipe.Any(c => c.RecipeId == p.RecipeId))
                    .ToList();
            }
            return RedirectToPage();

        }
    }
}
