using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public string Ingredient { get; set; }
        public string Quantity { get; set; }
    }
}
