using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using APS_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace APS_Project.Pages
{
    public class RecipeModel : PageModel
    {
        public Recipe Recipe { get; set; }
        private AppUser AppUser { get; set; }
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public List<Category> Checked { get; set; }

        public RecipeModel(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                AppUser = _dbContext.AppUsers.Find(userId);
            }
        }
        public async Task<IActionResult> OnPostAddCategoryAsync(string newCategory, int recipeId)
        {
            await LoadRecipe(recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                if (ModelState.IsValid)
                {
                    Recipe.CategoryRecipe.Add(new CategoryRecipe()
                    {
                        Category = new Category() { Name = newCategory },
                        Recipe = Recipe
                    });
                    await _dbContext.SaveChangesAsync();
                    return Page();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostEditCategoryAsync(string newCategory, int recipeId, int Checked)
        {
            await LoadRecipe (recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                if (newCategory is not null)
                {
                    Recipe.CategoryRecipe.FirstOrDefault(p => p.CategoryId == Checked).Category.Name = newCategory;
                    await _dbContext.SaveChangesAsync();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteCategoryAsync(List<int> Checked, int recipeId)
        {
            await LoadRecipe(recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                foreach (var check in Checked)
                {
                    _dbContext.Category.Remove(Recipe.CategoryRecipe.FirstOrDefault(p => p.CategoryId == check).Category);
                    _dbContext.CategoryRecipe.Remove(Recipe.CategoryRecipe.FirstOrDefault(p=>p.RecipeId == recipeId && p.CategoryId == check));
                }
                await _dbContext.SaveChangesAsync();
            }
            return Page();
        }
        public async Task OnGetAsync(int recipeId)
        {
            await LoadRecipe(recipeId);
        }
        private async Task LoadRecipe(int recipeId)
        {
            Recipe = await _dbContext.Recipes
                .Include(p => p.Links)
                .Include(p => p.RecipeOwner)
                .Include(p => p.CategoryRecipe)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(p => p.RecipeId == recipeId);
        }
    }
}
