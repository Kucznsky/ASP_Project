using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class UserDislikeRecipe
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
