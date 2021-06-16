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
        private AppUser AppUser { get; set; }
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
            await LoadRecipe(recipeId);
            if (Recipe.RecipeOwner == AppUser)
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
        public async Task<IActionResult> OnPostEditCategoryAsync(string newCategory, int recipeId, int Checked)
        {
            await LoadRecipe (recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                if (newCategory is not null)
                {
                    Recipe.CategoryRecipe.FirstOrDefault(p => p.CategoryId == Checked).Category.Name = newCategory;
                    await _dbContext.SaveChangesAsync();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteCategoryAsync(List<int> Checked, int recipeId)
        {
            await LoadRecipe(recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                foreach (var check in Checked)
                {
                    _dbContext.Category.Remove(Recipe.CategoryRecipe.FirstOrDefault(p => p.CategoryId == check).Category);
                    _dbContext.CategoryRecipe.Remove(Recipe.CategoryRecipe.FirstOrDefault(p=>p.RecipeId == recipeId && p.CategoryId == check));
                }
                await _dbContext.SaveChangesAsync();
            }
            return Page();
        }
        public async Task OnGetAsync(int recipeId)
        {
            await LoadRecipe(recipeId);
        }
        private async Task LoadRecipe(int recipeId)
        {
            Recipe = await _dbContext.Recipes
                .Include(p => p.Links)
                .Include(p => p.RecipeOwner)
                .Include(p => p.CategoryRecipe)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(p => p.RecipeId == recipeId);
        }
        public async Task<IActionResult> OnPostDeleteRecipeAsync(int recipeId)
        {
            string returnUrl = Url.Content("~/");
            await LoadRecipe(recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                _dbContext.Remove(Recipe);
            }
            await _dbContext.SaveChangesAsync();
            return LocalRedirect(returnUrl);
        }
        public async Task<IActionResult> OnPostEditAsync(int recipeId, string EditTitle, string EditDescription, string EditIndigrients)
        {
            string returnUrl = Url.Content("~/");
            await LoadRecipe(recipeId);
            if (Recipe.RecipeOwner == AppUser)
            {
                if (!string.IsNullOrEmpty(EditTitle))
                    Recipe.Title = EditTitle;
                if (!string.IsNullOrEmpty(EditDescription))
                    Recipe.Description = EditDescription;
                if (!string.IsNullOrEmpty(EditIndigrients)) 
                    Recipe.Indigrients = EditIndigrients;
                IFormFile file = Request.Form.Files[0];
                if (file is not null)
                {
                    byte[] filebuffer = new byte[file.Length];
                    var stream = file.OpenReadStream();
                    await stream.ReadAsync(filebuffer);
                    System.IO.File.WriteAllBytes("wwwroot/Data/Images/" + AppUser.Name + "_" + AppUser.LastName + "_" + Recipe.Title + ".jpg", filebuffer);
                }
                await _dbContext.SaveChangesAsync();
                return LocalRedirect(returnUrl);
            }
            return Page();
        }
    }
}
