using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class Catergory
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
