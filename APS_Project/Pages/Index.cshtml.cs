using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public AppUser AppUser { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
            {
                AppUser = _dbContext.AppUsers.Find(userId);
            }
            Recipes = new List<Recipe>();

        }
        public async Task OnGetAsync()
        {
            Recipes = await _dbContext.Recipes.OrderByDescending(p => p.RecipeLiker.Count - p.RecipeDisliker.Count).Take(Quantity).ToListAsync();
            List<UserLikeRecipe> ulr = _dbContext.UserLikeRecipes.ToList();
            List<UserDislikeRecipe> udr = _dbContext.UserDislikeRecipes.ToList();
            List<UserFollowRecipe> ufr = _dbContext.UserFollowRecipes.ToList();

        }
        public async Task<IActionResult> OnPostLikeAsync(int recipeId, bool like)
        {
            if (User.Identity.IsAuthenticated)
            {
                Recipe recipe = await _dbContext.Recipes.FindAsync(recipeId);
                UserLikeRecipe recipeLiker = _dbContext.UserLikeRecipes.FirstOrDefault(p => p.AppUserId == AppUser.Id && p.RecipeId == recipeId);
                UserDislikeRecipe recipeDisliker = _dbContext.UserDislikeRecipes.FirstOrDefault(p => p.AppUserId == AppUser.Id && p.RecipeId == recipeId);
                if (like)
                {
                    if (recipeLiker is not null)
                        recipe.RecipeLiker.Remove(recipeLiker);
                    else if (recipeDisliker is not null)
                    {
                        recipe.RecipeDisliker.Remove(recipeDisliker);
                        recipe.RecipeLiker.Add(new UserLikeRecipe() { Recipe = recipe, AppUser = AppUser });
                    }
                    else
                        recipe.RecipeLiker.Add(new UserLikeRecipe() { Recipe = recipe, AppUser = AppUser });
                }
                else
                {
                    if (recipeDisliker is not null)
                        recipe.RecipeDisliker.Remove(recipeDisliker);
                    else if (recipeLiker is not null)
                    {
                        recipe.RecipeLiker.Remove(recipeLiker);
                        recipe.RecipeDisliker.Add(new UserDislikeRecipe() { Recipe = recipe, AppUser = AppUser });
                    }
                    else
                        recipe.RecipeDisliker.Add(new UserDislikeRecipe() { Recipe = recipe, AppUser = AppUser });
                }
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
                Recipe recipe = await _dbContext.Recipes.FindAsync(recipeId);
                UserFollowRecipe ufr = _dbContext.UserFollowRecipes.FirstOrDefault(p => p.AppUserId == AppUser.Id && p.RecipeId == recipeId);
                if (ufr is null)
                    await _dbContext.UserFollowRecipes.AddAsync( new UserFollowRecipe() { Recipe = recipe, AppUser = AppUser});
                else
                    _dbContext.UserFollowRecipes.Remove(ufr);
                await _dbContext.SaveChangesAsync();
                return RedirectToPage();
            }
            else
                return Redirect("~/Identity/Account/Login");
        }

        public async Task<IActionResult> OnPostSearchAsync(string title, string author, string category)
        {
            List<Recipe> recipes = await _dbContext.Recipes.ToListAsync();
            if (!string.IsNullOrEmpty(title))
                recipes = recipes.Where(p => p.Title == title).ToList();

            if (!string.IsNullOrEmpty(author))
                recipes = recipes.Where(p => p.RecipeOwner.Name == author).ToList();

            if (!string.IsNullOrEmpty(category))
                recipes = recipes.Where(p => p.Categories.Any(c => c.Name == category)).ToList();

            Recipes = recipes;
            List<UserLikeRecipe> ulr = _dbContext.UserLikeRecipes.ToList();
            List<UserDislikeRecipe> udr = _dbContext.UserDislikeRecipes.ToList();
            List<UserFollowRecipe> ufr = _dbContext.UserFollowRecipes.ToList();
            return Page();
        }
    }
}
