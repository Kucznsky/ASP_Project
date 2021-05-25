using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryRecipe> CategoryRecipes { get; set; }
    }
}
