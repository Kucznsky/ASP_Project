using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class CategoryRelations
    {
        public IEnumerable<int> CategoryId { get; set; }
        public IEnumerable<int> RecipeId { get; set; }
    }
}
 