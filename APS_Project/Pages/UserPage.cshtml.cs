using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APS_Project.Pages
{
    public class UserPageModel : PageModel
    {
        public AppUser AppUser { get; set; }
        public List<Recipe> Recipes { get; set; }
        public int userId;
        private readonly ApplicationDbContext _dbContext;
        private readonly HttpContextAccessor _httpContextAccessor;
        public UserPageModel(HttpContextAccessor httpContextAccesor, ApplicationDbContext dbContext) 
        {
            _httpContextAccessor = httpContextAccesor;
            _dbContext = dbContext;
            int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        }

        public async Task OnGetAsync(int id)
        {
            AppUser = await _dbContext.AppUsers.FindAsync(id);
            Recipes = _dbContext.Recipes.Where(p => p.OwnerId == id).ToList();
        }
    }
}
