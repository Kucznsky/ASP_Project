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
            Recipes = await _dbContext.Recipes.OrderBy(p => p.Upvoters.Count - p.Downvoters.Count).Take(Quantity).ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(int recipeId, bool like)
        {
            if (User.Identity.IsAuthenticated)
            {
            Recipe recipe = await _dbContext.Recipes.FindAsync(recipeId);
            if (like)
            {
                if (recipe.Downvoters.Contains(AppUser))
                {
                    recipe.Downvoters.Remove(AppUser);
                    recipe.Upvoters.Add(AppUser);
                }
                else if(recipe.Upvoters.Contains(AppUser))
                {
                    recipe.Upvoters.Remove(AppUser);
                }
                else
                    recipe.Upvoters.Add(AppUser);
            }
            else 
            {
                if (recipe.Upvoters.Contains(AppUser)) 
                {
                    recipe.Upvoters.Remove(AppUser);
                    recipe.Downvoters.Add(AppUser);
                }
                else if (recipe.Downvoters.Contains(AppUser))
                {
                    recipe.Downvoters.Remove(AppUser);
                }
                else
                    recipe.Downvoters.Add(AppUser);
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
                if (AppUser.UserFavourites.Contains(recipe))
                {
                    AppUser.UserFavourites.Remove(recipe);
                }
                else 
                {
                    AppUser.UserFavourites.Add(recipe);
                }
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
