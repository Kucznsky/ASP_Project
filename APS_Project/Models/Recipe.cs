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
        public string Image { get; set; }
        [NotMapped]
        public Dictionary<string, string> IngredientsWithQuantity { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public List<int> Upvoters { get; private set; }
        [NotMapped]
        public List<int> Downvoters { get; private set; }
        public DateTime PublicationDate { get;set; }
        public DateTime LastEditDate { get;set; }
        public int OwnerId { get; set; }
        [NotMapped]
        public List<string> GaleryLinks { get; set; }

        public void UpvoteClick(int UserId)
        {
            if (Upvoters.Contains(UserId))
            {
                Upvoters.Remove(UserId);
            }
            else if (Downvoters.Contains(UserId))
            {
                Downvoters.Remove(UserId);
                Upvoters.Add(UserId);
            }
            else 
            {
                Upvoters.Add(UserId);
            }
        }
        public void DownvoteClick(int UserId)
        {
            if (Downvoters.Contains(UserId))
            {  
                Downvoters.Remove(UserId);
            }
            else if (Upvoters.Contains(UserId))
            {
                Upvoters.Remove(UserId);
                Downvoters.Add(UserId);
            }
            else
            {
                Downvoters.Add(UserId);
            }
        }
        public Recipe() 
        {
            
        
        
        }
    }
}
