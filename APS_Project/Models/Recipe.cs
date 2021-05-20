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
        [Required]
        public string Title { get; set; }
        //[Required]
        public string? ImageName { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime PublicationDate { get;set; }
        public DateTime LastEditDate { get;set; }
        public int OwnerId { get; set; }
        [NotMapped]
        public int Likes { get; set; }
        [NotMapped]
        public bool IsUserFavourite { get; set; }

        public Recipe()
        {
            PublicationDate = DateTime.Now;
            LastEditDate = PublicationDate;
            Likes = 0;
        }
    }
}
