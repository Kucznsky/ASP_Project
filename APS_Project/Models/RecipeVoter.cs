using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class RecipeVoter
    { 
        public int RecipeId { get; set; }
        public int VoterId { get; set; }
        public bool Vote { get; set; }
    }    
}
