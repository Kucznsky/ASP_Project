using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class User
    {
        int UserId { get; set; }
        [ForeignKey("RecipeId")]
        public IEnumerable<int> ListOfFavs { get; set; }
        [ForeignKey("RecipeId")]
        public IEnumerable<int> ListOfMyRecepies {get;set;}
        string Name { get; set; }
        string LastName { get; set; }
        string Email { get; set;}
        string RegistrationDate { get; set; }
        string Description { get; set; }
    }
}
