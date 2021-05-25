using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class Link
    {
        public int RecipeId { get; set; }
        public Recipe Recpie { get; set; }
        public int Id { get; set; }
        public string LinkToImage { get; set; }
    }
}
