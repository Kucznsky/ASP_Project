using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeId { get; set; }
        [Required]
        public string Ingredient { get; set; }
        [Required]
        public string Quantity { get; set; }
    }
}
