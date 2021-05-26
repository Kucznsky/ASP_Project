using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    [Authorize]
    public class PublishModel : PageModel
    {
        public string ERROR { get; set; }
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUser AppUser { get; set; }
        [BindProperty]
        public InputModel inputModel { get; set; }
        public class InputModel
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public string Categories { get; set; }
            [Required]
            public string Ingredient { get; set; }
        }
        public PublishModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                AppUser = _dbContext.AppUsers.Find(userId);
            }
            ERROR = "";
            _dbContext = context;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!_dbContext.Recipes.Any(p => p.Title == inputModel.Title))
                {
                    IFormFile file;
                    if (Request.Form.Files.Count > 0)
                    {
                        file = Request.Form.Files[0];
                        byte[] filebuffer = new byte[file.Length];
                        Recipe recipe = new()
                        {
                            Description = inputModel.Description,
                            Title = inputModel.Title,
                            ImageName = AppUser.Name + "_" + AppUser.LastName + "_" + inputModel.Title + ".jpg",
                            RecipeOwner = AppUser,
                            RecipeOwnerId = AppUser.Id,
                            Indigrients = inputModel.Ingredient,
                        };
                        string[] categories = inputModel.Categories.Split();
                        List<Category> Cat = new();
                        foreach (var cat in categories)
                        {
                            Cat.Add(new Category() { Name = cat });
                        }
                        await _dbContext.Recipes.AddAsync(recipe);
                        await _dbContext.Category.AddRangeAsync(Cat);
                        await _dbContext.SaveChangesAsync();
                        var recWithId = _dbContext.Recipes.FirstOrDefault(p => p.Title == inputModel.Title);
                        if(recWithId is not null)
                            foreach (var cat in Cat)
                            {
                                _dbContext.CategoryRecipe.Add(new CategoryRecipe()
                                {
                                    Category = _dbContext.Category.FirstOrDefault(p => p.Name == cat.Name),
                                    Recipe = recWithId
                                });
                            }
                        await _dbContext.SaveChangesAsync();
                        var stream = file.OpenReadStream();
                        await stream.ReadAsync(filebuffer);
                        System.IO.File.WriteAllBytes("wwwroot/Data/Images/" + AppUser.Name + "_" + AppUser.LastName + "_" + inputModel.Title + ".jpg", filebuffer);
                        try
                        {
                            await _dbContext.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            ERROR += "Error while saving";
                            Console.WriteLine(ex);
                        }
                    }
                    else
                    {
                        ERROR += "You must add a file";
                        return Page();
                    }
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
