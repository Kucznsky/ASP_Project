using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    public class PublishModel : PageModel
    {
        private readonly ILogger<PublishModel> _logger;
        public string ERROR { get; set; }
        private readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Recipe Recipe { get; set; }
        [BindProperty]
        public Category Category { get; set; }
        [BindProperty]
        public RecipeIngredient RecipeIngredient { get; set; }
        public PublishModel(ILogger<PublishModel> logger, ApplicationDbContext context)
        {
            ERROR = "";
            _logger = logger;
            _dbContext = context;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var files = Request.Form.Files;
            if (ModelState.IsValid)
            {
                if (_dbContext.Catergories.Where(p => p.Name == Category.Name) is null)
                    await _dbContext.Catergories.AddAsync(Category);

                if (_dbContext.Recipes.Where(p => p.Title == Recipe.Title) is null)
                {
                    await _dbContext.RecipeIngredients.AddAsync(RecipeIngredient);
                    await _dbContext.Recipes.AddAsync(Recipe);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    ERROR += "We have recipe with the same title.";
                    return Page();
                }
            }
            return RedirectToPage("./Index");
        }
        public void OnGet()
        {
        }
    }
}
