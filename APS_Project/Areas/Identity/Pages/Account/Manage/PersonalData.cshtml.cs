using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace APS_Project.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        public PersonalDataModel(
            UserManager<AppUser> userManager,
            ILogger<PersonalDataModel> logger,
             ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _logger = logger;
            _dbContext = dbContext;
        }
        public AppUser Userr;
        [BindProperty]
        public string Description { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public string Image { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            Userr = user;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostImageAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            IFormFile file;
            if (Request.Form.Files.Count > 0)
            { 
                file = Request.Form.Files[0];
                byte[] filebuffer = new byte[file.Length];
                user.UserPicture = user.Name + "_" + user.LastName + "_" + ".jpg";
                await _dbContext.SaveChangesAsync();
                var stream = file.OpenReadStream();
                await stream.ReadAsync(filebuffer);
                System.IO.File.WriteAllBytes("wwwroot/Data/Images/" + user.Name + "_" + user.LastName + "_" + ".jpg", filebuffer);
                await _dbContext.SaveChangesAsync();
                StatusMessage = "Your profile has been updated";
                Userr = user;
                return RedirectToPage();          
            }
            else
            {
                return RedirectToPage();
            }
        }
        public async Task<IActionResult> OnPostDescAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.Description = Description;
            await _dbContext.SaveChangesAsync();
            StatusMessage = "Your profile has been updated";
            Userr = user;
            return RedirectToPage();
        }
    }
}