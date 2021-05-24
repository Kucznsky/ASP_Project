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
        [Key]
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get;set; }
        public DateTime LastEditDate { get;set; }
        [ForeignKey("Owner")]
        public AppUser RecipeOwner { get; set; }
        public int RecipeOwnerId { get; set; }
        public List<Category> Categories { get; set; } 
        public string Indigrients { get; set; }
        [ForeignKey("Upvoters")]
        public List<AppUser> Upvoters { get; set; }
        [ForeignKey("Downvoters")]
        public List<AppUser> Downvoters { get; set; }
        [ForeignKey("GaleryLinks")]
        public List<Link> Links { get; set; }

        public Recipe()
        {
            PublicationDate = DateTime.Now;
            LastEditDate = PublicationDate;
            Categories = new List<Category>();
            Upvoters = new List<AppUser>();
            Downvoters = new List<AppUser>();
            Links = new List<Link>();
        }
    }
}
