using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class UserFollowRecipe
    {
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
    }
}
