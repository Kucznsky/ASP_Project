using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class CategoryRecipe
    {
        public int CategoryId { get; set; }
        public int RecipeId { get; set; }
    }
}
 