using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APS_Project.Data;
using APS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace APS_Project.Pages
{
    public class PublishModel : PageModel
    {
        private readonly ILogger<PublishModel> _logger;
        private readonly ApplicationDbContext _dbContext;
        public Recipe Recipe { get; set; }
        public Catergory Category { get; set; }
        public RecipeIngredient RecipeIngredient { get; set; }
        public PublishModel(ILogger<PublishModel> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public void OnGet()
        {
        }
    }
}
