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
        public DbSet<CategoryRelation> CategoryRelations { get; set; }
        public DbSet<Catergory> Catergories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
