using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Pages
{
    public class IndexModel : PageModel
    {
        const int Quantity = 10; 
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        public List<Recipe> Recipes { get; set; }
        

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
             Recipes = await _dbContext.Recipes.ToListAsync();
        }
    }
}
