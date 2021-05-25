using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class AppUser : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string UserPicture { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Description { get; set; }
        public List<UserFollowRecipe> UserFollows { get; set; }
        public List<Recipe> UserRecipes { get; set; }
        public List<UserLikeRecipe> UserLikes { get; set; }
        public List<UserDislikeRecipe> UserDislikes { get; set; }

        public AppUser() : base()
        {
            UserLikes = new List<UserLikeRecipe>();
            UserDislikes = new List<UserDislikeRecipe>();
            UserFollows = new List<UserFollowRecipe>();
            UserDislikes = new List<UserDislikeRecipe>();
        
        }
    }
}
