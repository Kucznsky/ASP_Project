using APS_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APS_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        DbSet<CategoryRelations> CategoryRelations { get; set; }
        DbSet<Catergory> Catergories { get; set; }
        DbSet<Recipe> Recipes { get; set; }
        DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
