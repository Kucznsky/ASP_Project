using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace APS_Project.Pages
{
    public class RecipeModel : PageModel
    {
        public Recipe Recipe { get; set; }
        public AppUser RecipeOwner { get; set; }
        public CategoryRecipe Category { get; set; }
        private readonly ApplicationDbContext _dbContext;
        public RecipeModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToPage();
        }
        public async Task OnGetAsync(int recipeId)
        {
            _ = await _dbContext.Category.ToListAsync();
            _ = await _dbContext.CategoryRecipe.ToListAsync();
            Recipe =  await _dbContext.Recipes.FindAsync(recipeId);
            Recipe.CategoryRecipe = await _dbContext.CategoryRecipe.Where(p => p.RecipeId == Recipe.RecipeId).ToListAsync();
            RecipeOwner = await _dbContext.AppUsers.FindAsync(Recipe.RecipeOwnerId);
        }
    }
}
