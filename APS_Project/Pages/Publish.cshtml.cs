using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    [Authorize]
    public class PublishModel : PageModel
    {
        private readonly ILogger<PublishModel> _logger;
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
            public string CategoryName { get; set; }
            [Required]
            public string Ingredient { get; set; }
        }
        public PublishModel(ILogger<PublishModel> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                AppUser = _dbContext.AppUsers.Find(userId);
            }
            ERROR = "";
            _logger = logger;
            _dbContext = context;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!_dbContext.Recipes.Any(p => p.Title == inputModel.Title))
                {
                    var file = Request.Form.Files[0];
                    byte[] filebuffer = new byte[file.Length];
                    Recipe recipe = new()
                    {
                        Description = inputModel.Description,
                        Title = inputModel.Title,
                        ImageName = AppUser.Name + "_" + AppUser.LastName + "_" + inputModel.Title + ".jpg",
                        RecipeOwner = AppUser,
                        RecipeOwnerId = AppUser.Id,
                        Indigrients = inputModel.Ingredient
                    };
                    recipe.Categories.Add(new Category() { Name = inputModel.CategoryName });
                    await _dbContext.Recipes.AddAsync(recipe);
                    var stream = file.OpenReadStream();
                    await stream.ReadAsync(filebuffer);
                    System.IO.File.WriteAllBytes("wwwroot/Data/Images/" + AppUser.Name + "_" + AppUser.LastName + "_" + inputModel.Title + ".jpg", filebuffer);
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
