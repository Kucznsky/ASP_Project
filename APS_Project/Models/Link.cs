using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_Project.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LinkToImage { get; set; }
    }
}
