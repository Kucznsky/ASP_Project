using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APS_Project.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [BindProperty]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }



        private async Task LoadAsync(AppUser user)
        {
            var usr = await _dbContext.AppUsers.FindAsync(user.Id);
            var userName = usr.Name;
            var lastName = usr.LastName;

            Name = userName;
            LastName = lastName;
           
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var lastName = LastName;
            if (LastName != user.LastName)
            {
                var appUser = await _dbContext.AppUsers.FindAsync(user.Id);
                appUser.LastName = lastName;
            }
            var name = Name;
            if (Name != user.Name)
            {
                var appUser = await _dbContext.AppUsers.FindAsync(user.Id);
                appUser.Name = Name;
            }
            await _dbContext.SaveChangesAsync();
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
