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
    public class MyRecipesModel : PageModel
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUser AppUser { get; set; }
        public List<Recipe> Recipes { get; set; }

        public MyRecipesModel(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                AppUser = _dbContext.AppUsers
                    .FirstOrDefault(p => p.Id == userId);
            }
            Recipes = new();
        }
        private async Task LoadRecipes() 
        {
            Recipes = await _dbContext.Recipes
                .Include(p => p.RecipeDisliker)
                .Include(p => p.RecipeFollower)
                .Include(p => p.RecipeLiker)
                .Where(p => p.RecipeOwnerId == AppUser.Id)
                .ToListAsync();

        }
        public async Task OnGetAsync()
        {
            await LoadRecipes();
        }
        public async Task<IActionResult> OnPostSearchAsync(string category, DateTime startTime, DateTime endTime)
        {
            await LoadRecipes();
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
                Recipes = Recipes
                    .Where(p => p.CategoryRecipe
                        .Any(c => c.Category.Name == category))
                    .ToList();
            }
            return Page();
        }
    }
}
