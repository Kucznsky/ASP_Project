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
using Microsoft.Extensions.Logging;

namespace APS_Project.Pages
{
    public class UserProfileModel : PageModel
    {

        private int id { get; set; }
        public CategoryRecipe Category { get; set; }
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserProfileModel(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;


        }
        public void OnGet()
        {
        }
        public AppUser UserProfile;
    }
}
