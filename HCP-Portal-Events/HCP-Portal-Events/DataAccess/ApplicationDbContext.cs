using HCP_Portal_Events.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyApiProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            
        }

    }
}