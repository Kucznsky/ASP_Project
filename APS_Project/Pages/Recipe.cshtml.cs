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
        public AppUser RecipeOwner { get; set; }
        private AppUser AppUser { get; set; }
        public CategoryRecipe Category { get; set; }
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
            _ = await _dbContext.Category.ToListAsync();
            _ = await _dbContext.CategoryRecipe.ToListAsync();
            Recipe = await _dbContext.Recipes.FindAsync(recipeId);
            RecipeOwner = await _dbContext.AppUsers.FindAsync(Recipe.RecipeOwnerId);
            if (RecipeOwner == AppUser)
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
        public async Task<IActionResult> OnPostEditCategoryAsync(string newCategory, int categoryId, int recipeId)
        {
            _ = await _dbContext.Category.ToListAsync();
            _ = await _dbContext.CategoryRecipe.ToListAsync();
            Recipe = await _dbContext.Recipes.FindAsync(recipeId);
            RecipeOwner = await _dbContext.AppUsers.FindAsync(Recipe.RecipeOwnerId);
            if (RecipeOwner == AppUser)
            {
                if (ModelState.IsValid)
                {
                    if (_dbContext.CategoryRecipe.Where(p => p.Category.Id == categoryId).Count() > 1)
                    {
                        var cr = await _dbContext.CategoryRecipe.FindAsync(Recipe.RecipeId, categoryId);
                        _dbContext.CategoryRecipe.Remove(cr);
                        var cat = new Category() { Name = newCategory };
                        await _dbContext.Category.AddAsync(cat);
                        _dbContext.CategoryRecipe.Add(new CategoryRecipe() { Category = cat, Recipe = Recipe });
                    }
                    else
                        _dbContext.Category.Find(categoryId).Name = newCategory;
                    await _dbContext.SaveChangesAsync();
                    return Page();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteCategoryAsync(int categoryId, int recipeId)
        {
            _ = await _dbContext.Category.ToListAsync();
            _ = await _dbContext.CategoryRecipe.ToListAsync();
            Recipe = await _dbContext.Recipes.FindAsync(recipeId);
            RecipeOwner = await _dbContext.AppUsers.FindAsync(Recipe.RecipeOwnerId);
            if (RecipeOwner == AppUser)
            {
                if (ModelState.IsValid)
                {
                    var c = await _dbContext.Category.FindAsync(categoryId);
                    var cr = await _dbContext.CategoryRecipe.FindAsync(Recipe.RecipeId, categoryId);
                    _dbContext.CategoryRecipe.Remove(cr);
                    _dbContext.Category.Remove(c);
                    await _dbContext.SaveChangesAsync();
                    return Page();
                }
            }
            return Page();
        }
        public async Task OnGetAsync(int recipeId)
        {
            _ = await _dbContext.Category.ToListAsync();
            _ = await _dbContext.CategoryRecipe.ToListAsync();
            Recipe =  await _dbContext.Recipes.FindAsync(recipeId);
            RecipeOwner = await _dbContext.AppUsers.FindAsync(Recipe.RecipeOwnerId);
        }
    }
}
