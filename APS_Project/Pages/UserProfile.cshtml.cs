using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APS_Project.Areas.Identity.Pages.Account.Manage;
using APS_Project.Data;
using APS_Project.Models;
using APS_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APS_Project.Pages
{
    public class UserProfileModel : PageModel
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUser AppUser { get; set; }
        public AppUser UserProfile { get; set; }
        public List<Recipe> Recipes { get; set; }
        public UserProfileModel(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                AppUser = await _dbContext.AppUsers
                    .Include(p=>p.UserRecipes)
                    .ThenInclude(p=>p.RecipeLiker)
                    .Include(p=>p.UserRecipes)
                    .ThenInclude(p=>p.RecipeDisliker)
                    .Include(p => p.UserRecipes)
                    .ThenInclude(p => p.RecipeFollower)
                    .FirstOrDefaultAsync(p=>p.Id == id);
               
            }
            return Page();
        }
    }
}
