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
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime PublicationDate { get;set; }
        public DateTime LastEditDate { get;set; }
        public int OwnerId { get; set; }

        public Recipe(int ownerId)
        {
            PublicationDate = DateTime.Now;
            LastEditDate = PublicationDate;
            OwnerId = ownerId;
        }
    }
}
