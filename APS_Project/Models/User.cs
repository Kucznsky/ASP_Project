using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class User
    {
        public int UserId { get; set; }
        //[ForeignKey("RecipeId")]
        public IEnumerable<int> ListOfFavs { get; set; }
        //[ForeignKey("RecipeId")]
        public IEnumerable<int> ListOfMyRecepies {get;set;}
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set;}
        public string RegistrationDate { get; set; }
        public string Description { get; set; }
    }
}
