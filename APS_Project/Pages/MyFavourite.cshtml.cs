using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    [Authorize]
    public class MyFavouriteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUser AppUser { get; set; }
        public List<Recipe> Recipes { get; set; }

        public MyFavouriteModel(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                AppUser = _dbContext.AppUsers.Find(userId);
            }
        }
        public async Task OnGetAsync()
        {
            Recipes = await _dbContext.Recipes
                .Where(p => _dbContext.UserFollowRecipes
                    .Any(c => p.RecipeId == c.RecipeId && c.AppUserId == AppUser.Id))
                .ToListAsync();
            List<UserLikeRecipe> ulr = _dbContext.UserLikeRecipes.ToList();
            List<UserDislikeRecipe> udr = _dbContext.UserDislikeRecipes.ToList();
            List<UserFollowRecipe> ufr = _dbContext.UserFollowRecipes.ToList();
        }
        public async Task<IActionResult> OnPostSearchAsync(string category, DateTime startTime, DateTime endTime)
        {
            _ = await _dbContext.Recipes.ToListAsync();
            _ = await _dbContext.Category.ToListAsync();
            Recipes = AppUser.UserRecipes;
            if (startTime != DateTime.MinValue)
            {
                Recipes = Recipes
                    .Where(p => p.PublicationDate >= startTime)
                    .ToList();
            }
            if (endTime != DateTime.MinValue)
            {
                Recipes = Recipes
                    .Where(p => p.PublicationDate <= endTime)
                    .ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                var cat = _dbContext.Category.First(p => p.Name == category);
                var catRec = _dbContext.CategoryRecipe.FirstOrDefault(c => c.CategoryId == cat.Id);
                Recipes = Recipes.Where(p => p.RecipeId == catRec.RecipeId).ToList();
            }
            if (Recipes is null)
                Recipes = new List<Recipe>();
            return Page();
        }
    }
}
