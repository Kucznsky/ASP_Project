using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APS_Project.Pages
{
    public class RecipeModel : PageModel
    {
        public Recipe Recipe { get; set; }
        public RecipeIngredient RecipeIngredient { get; set; }
        public Category Category { get; set; }
        private readonly ApplicationDbContext _dbContext;
        public RecipeModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task OnGetAsync(int recipeId)
        {
            Recipe =  await _dbContext.Recipes.FindAsync(recipeId);
            RecipeIngredient = await _dbContext.RecipeIngredients.FindAsync(recipeId);
            Category = await _dbContext.Categories.FindAsync(recipeId);
        }
    }
}
