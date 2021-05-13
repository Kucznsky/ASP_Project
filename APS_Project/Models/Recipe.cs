using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public Dictionary<string, string> IngredientsWithQuantity { get; set; }
        public string Description { get; set; }
        public List<int> Upvoters { get; set; }
        public List<int> Downvoters { get; set; }
        public DateTime PublicationDate { get;set; }
        public DateTime LastEditDate { get;set; }
        public int OwnerId { get; set; }
        public List<string> GaleryLinks { get; set; }

    }
}
