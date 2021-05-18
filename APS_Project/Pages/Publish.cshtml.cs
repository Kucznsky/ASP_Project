using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace APS_Project.Pages
{
    public class PublishModel : PageModel
    {
        private readonly ILogger<PublishModel> _logger;
        private readonly ApplicationDbContext _context;
        public Recipe Recipe { get; set; }
        public Catergory Category { get; set; }
        public RecipeIngredient RecipeIngredient { get; set; }
        public PublishModel(ILogger<PublishModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Catergories.Add(Category);
                _context.SaveChanges();
                _context.Recipes.Add(Recipe);
                _context.SaveChanges();
                _context.RecipeIngredients.Add(RecipeIngredient);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
        public void OnGet()
        {
        }
    }
}
