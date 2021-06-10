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
            public string Links { get; set; }
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
                        List<Category> Cat = new();
                        List<Link> Links = new();
                        List<CategoryRecipe> CR = new();
                        Recipe recipe = new()
                        {
                            Description = inputModel.Description,
                            Title = inputModel.Title,
                            ImageName = AppUser.Name + "_" + AppUser.LastName + "_" + inputModel.Title + ".jpg",
                            RecipeOwner = AppUser,
                            Indigrients = inputModel.Ingredient,
                            CategoryRecipe = CR,
                            Links = Links
                        };
                        string[] categories = inputModel.Categories.Split();
                        string[] links = inputModel.Links.Split();
                        foreach (var cat in categories)
                        {
                            CR.Add(new CategoryRecipe()
                            {
                                Category = new Category() { Name = cat },
                                Recipe = recipe
                            });
                        }
                        foreach (var link in links)
                        {
                            Links.Add(new Link() { LinkToImage = link, Recpie = recipe });
                        }
                        await _dbContext.Recipes.AddAsync(recipe);
                        await _dbContext.SaveChangesAsync();
                        var stream = file.OpenReadStream();
                        await stream.ReadAsync(filebuffer);
                        System.IO.File.WriteAllBytes("wwwroot/Data/Images/" + AppUser.Name + "_" + AppUser.LastName + "_" + inputModel.Title + ".jpg", filebuffer);
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
