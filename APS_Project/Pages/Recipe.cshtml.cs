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
        private readonly ApplicationDbContext _dbContext;
        public RecipeModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task OnGetAsync()
        {
            Recipe =  await _dbContext.Recipes.FindAsync(int.Parse(Request.Query["recipeId"]));
        }
    }
}
