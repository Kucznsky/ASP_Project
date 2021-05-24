using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Http;
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
            Recipes = await _dbContext.Recipes.OrderBy(p => p.RecipeLiker.Count - p.RecipeDisliker.Count).Take(Quantity).ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(int recipeId, bool like)
        {
            if (User.Identity.IsAuthenticated)
            {
                Recipe recipe = await _dbContext.Recipes.FindAsync(recipeId);
                if (like)
                {
                    if (AppUser.UserLikes.Any(p=>p.AppUser == AppUser))
                    {
                        var recipeliker = _dbContext.UserLikeRecipes.First(p => p.AppUserId == AppUser.Id);
                        recipe.RecipeLiker.Remove(recipeliker);
                    }
                    else if (recipe.RecipeDisliker.Any(p => p.AppUserId == AppUser.Id))
                    {
                        var recipedisliker = _dbContext.UserDislikeRecipes.First(p => p.AppUserId == AppUser.Id);
                        recipe.RecipeDisliker.Remove(recipedisliker);
                        recipe.RecipeLiker.Add(new UserLikeRecipe() { Recipe = recipe, AppUser = AppUser });

                    }
                    else
                        recipe.RecipeLiker.Add(new UserLikeRecipe() { Recipe = recipe, AppUser = AppUser });

                }
                else
                {
                    if (recipe.RecipeDisliker.Any(p => p.AppUserId == AppUser.Id))
                    {
                        var recipedisliker = _dbContext.UserDislikeRecipes.First(p => p.AppUserId == AppUser.Id);
                        recipe.RecipeDisliker.Remove(recipedisliker);
                    }
                    else if (recipe.RecipeLiker.Any(p => p.AppUserId == AppUser.Id))
                    {
                        var recipeliker = _dbContext.UserLikeRecipes.First(p => p.AppUserId == AppUser.Id);
                        recipe.RecipeLiker.Remove(recipeliker);
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
                if(User)

                return RedirectToPage();
            }
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }

        public async Task<IActionResult> OnGetSearchAsync(string title, string author, string category)
        {
            List<Recipe> recipes = await _dbContext.Recipes.ToListAsync();
            if (!string.IsNullOrEmpty(title))
            {
                recipes = recipes.Where(p => p.Title == title).ToList();
            }
            if (!string.IsNullOrEmpty(author))
            {
                recipes = recipes.Where(p => p.RecipeOwner.Name == author).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                recipes = recipes.Where(p => p.Categories.Any(c => c.Name == category)).ToList();
            }
            Recipes = recipes;
            return Page();
        }
    }
}
