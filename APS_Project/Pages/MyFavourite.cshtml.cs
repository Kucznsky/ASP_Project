using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    public class MyFavouriteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public List<Recipe> Recipes { get; set; }
        public List<UserFavourite> UserFavourites { get; set; }

        public MyFavouriteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Recipes = new List<Recipe>();
        }
        public async Task OnGetAsync(int userId)
        {
            UserFavourites = await _dbContext.UserFavourites
                .Where(p => p.UserId == userId)
                .ToListAsync();
            foreach (var userFav in UserFavourites)
            {
                Recipes.Add(_dbContext.Recipes
                    .Single(p => p.RecipeId == userFav.RecipeId));
            }
        }
        public async Task<IActionResult> OnPostSearchAsync(string category, DateTime startTime, DateTime endTime)
        {

            Recipes = await _dbContext.Recipes.ToListAsync();
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
                var Category = await _dbContext.Catergories
                    .Where(p => p.Name == category)
                    .ToListAsync();
                var CategoryRecipe = await _dbContext.CategoryRecipe
                    .Where(p => Category.Any(c => c.CategoryID == p.CategoryId))
                    .ToListAsync();
                Recipes = Recipes
                    .Where(p => CategoryRecipe.Any(c => c.RecipeId == p.RecipeId))
                    .ToList();
            }
            return RedirectToPage();
        }
    }
}
