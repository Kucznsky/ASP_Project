using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    public class IndexModel : PageModel
    {
        const int Quantity = 10;
        //private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        public List<Recipe> Recipes { get; set; }


        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
        {
            // _logger = logger;
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            //tym dodajecie jedna recipe
            /*_dbContext.Recipes.Add(new Recipe(1)
            {
                Title = "Best burgi",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ImageName = "burger.jpg",
            });
            await _dbContext.SaveChangesAsync();
            */
            Recipes = await _dbContext.Recipes.OrderBy(p => p.RecipeLikes).Take(Quantity).ToListAsync();
            foreach (var recipe in Recipes)
            {
                recipe.RecipeLikes = _dbContext.RecipeLikes(recipe.RecipeId);
                recipe.IsUserFavourite = await _dbContext.IsRecipeUserFavourite(recipe.RecipeId, 13);
            }
        }

        public async Task Like()
        {
            
        }
    }
}
