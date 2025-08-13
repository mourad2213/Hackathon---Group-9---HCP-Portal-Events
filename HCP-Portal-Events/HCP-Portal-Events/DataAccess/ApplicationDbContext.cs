using HCP_Portal_Events.DataAccess.Configurations;
using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyApiProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<EventStatus> EventStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activites { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<UserRegistrationToEvent> UserRegistrationToEvents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserRegistrationToEventConfiguration());
        }
    }
}