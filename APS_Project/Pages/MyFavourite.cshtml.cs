using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            UserFavourites = await _dbContext.UserFavourites.Where(p => p.UserId == userId).ToListAsync();
            foreach (var userFav in UserFavourites)
            {
                Recipes.Add(_dbContext.Recipes.Single(p => p.RecipeId == userFav.RecipeId));
            }
        }
    }
}
