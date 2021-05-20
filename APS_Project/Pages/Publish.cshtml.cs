using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace APS_Project.Pages
{
    [Authorize]
    public class PublishModel : PageModel
    {
        private readonly ILogger<PublishModel> _logger;
        private readonly ApplicationDbContext _context;
        [BindProperty] 
        public Recipe Recipe { get; set; }
        [BindProperty] 
        public Category Category { get; set; }
        [BindProperty] 
        public RecipeIngredient RecipeIngredient { get; set; }
        public PublishModel(ILogger<PublishModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task <IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //var log=img;
                await _context.Catergories.AddAsync(Category);
                await _context.Recipes.AddAsync(Recipe);
                await _context.RecipeIngredients.AddAsync(RecipeIngredient);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
        public void OnGet()
        {
        }
    }
}
