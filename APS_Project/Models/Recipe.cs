using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get;set; }
        public DateTime LastEditDate { get;set; }
        public int RecipeOwnerId { get; set; }
        public AppUser RecipeOwner { get; set; }
        public string Indigrients { get; set; }
        public List<CategoryRecipe> CategoryRecipe { get; set; } 
        public List<UserLikeRecipe> RecipeLiker { get; set; }
        public List<UserDislikeRecipe> RecipeDisliker { get; set; }
        public List<UserFollowRecipe> RecipeFollower { get; set; }
        public List<Link> Links { get; set; }

        public Recipe()
        {
            PublicationDate = DateTime.Now;
            LastEditDate = PublicationDate;
            CategoryRecipe = new();
            RecipeDisliker = new();
            RecipeFollower = new();
            RecipeLiker = new();
        }
    }
}
