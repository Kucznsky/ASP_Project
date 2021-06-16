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
        }
        public async Task OnGetAsync()
        {
            await LoadRecipes();
            Recipes = Recipes
                .OrderByDescending(p => p.RecipeLiker.Count - p.RecipeDisliker.Count)
                .Take(Quantity)
                .ToList();
        }
        public async Task<IActionResult> OnPostLikeAsync(int recipeId, bool like)
        {
            if (User.Identity.IsAuthenticated)
            {
                Recipe recipe = await _dbContext.Recipes
                    .Include(p => p.RecipeDisliker)
                    .Include(p => p.RecipeLiker)
                    .FirstOrDefaultAsync(p => p.RecipeId == recipeId);
                UserLikeRecipe recipeLiker = recipe.RecipeLiker
                    .FirstOrDefault(p => p.AppUserId == AppUser.Id);
                UserDislikeRecipe recipeDisliker = recipe.RecipeDisliker
                    .FirstOrDefault(p => p.AppUserId == AppUser.Id);
                if (like)
                {
                    if (recipeLiker is not null)
                        recipe.RecipeLiker.Remove(recipeLiker);
                    else if (recipeDisliker is not null)
                    {
                        recipe.RecipeDisliker.Remove(recipeDisliker);
                        recipe.RecipeLiker.Add(new UserLikeRecipe()
                        {
                            Recipe = recipe,
                            AppUser = AppUser
                        });
                    }
                    else
                        recipe.RecipeLiker.Add(new UserLikeRecipe()
                        {
                            Recipe = recipe,
                            AppUser = AppUser
                        });
                }
                else
                {
                    if (recipeDisliker is not null)
                        recipe.RecipeDisliker.Remove(recipeDisliker);
                    else if (recipeLiker is not null)
                    {
                        recipe.RecipeLiker.Remove(recipeLiker);
                        recipe.RecipeDisliker.Add(new UserDislikeRecipe()
                        {
                            Recipe = recipe,
                            AppUser = AppUser
                        });
                    }
                    else
                        recipe.RecipeDisliker.Add(new UserDislikeRecipe()
                        {
                            Recipe = recipe,
                            AppUser = AppUser
                        });
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
                Recipe recipe = await _dbContext.Recipes
                    .Include(p => p.RecipeFollower)
                    .FirstOrDefaultAsync(p => p.RecipeId == recipeId);
                UserFollowRecipe ufr = recipe.RecipeFollower
                    .FirstOrDefault(p => p.AppUserId == AppUser.Id);
                if (ufr is null)
                    await _dbContext.UserFollowRecipes.AddAsync(new UserFollowRecipe()
                    {
                        Recipe = recipe,
                        AppUser = AppUser
                    });
                else
                    _dbContext.UserFollowRecipes.Remove(ufr);
                await _dbContext.SaveChangesAsync();
                return RedirectToPage();
            }
            else
                return Redirect("~/Identity/Account/Login");
        }
        private async Task LoadRecipes()
        {
            Recipes = await _dbContext.Recipes
                .Include(p => p.RecipeOwner)
                .Include(p => p.RecipeDisliker)
                .Include(p => p.RecipeFollower)
                .Include(p => p.RecipeLiker)
                .Include(p => p.CategoryRecipe)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostSearchAsync(string title, string author, string category)
        {
            await LoadRecipes();
            if (!string.IsNullOrEmpty(title))
                Recipes = Recipes
                    .Where(p => p.Title == title)
                    .ToList();
            if (!string.IsNullOrEmpty(author))
                Recipes = Recipes
                    .Where(p => p.RecipeOwner.Name == author)
                    .ToList();
            if (!string.IsNullOrEmpty(category))
                Recipes = Recipes
                    .Where(p => p.CategoryRecipe
                        .Any(p => p.Category.Name == category))
                    .ToList();
            return Page();
        }
    }
}
