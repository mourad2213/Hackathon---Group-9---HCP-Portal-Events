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
        public DbSet<ActivitySpeaker> ActivitySpeakers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserRegistrationToEventConfiguration());
            modelBuilder.ApplyConfiguration(new ActivitySpeakerConfiguration());
        }
        private static void SeedData(ModelBuilder modelBuilder)
        {
            var now = DateTime.Now;

            modelBuilder.Entity<Speciality>().HasData(
                new Speciality { Id = 1, Field = "Cardiology" },
                new Speciality { Id = 2, Field = "Neurology" },
                new Speciality { Id = 3, Field = "Pediatrics" },
                new Speciality { Id = 4, Field = "Oncology" },
                new Speciality { Id = 5, Field = "General Practice" }
            );

            modelBuilder.Entity<EventType>().HasData(
                new EventType { Id = 1, Type = "CME" },
                new EventType { Id = 2, Type = "Webinar" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "dr_smith",
                    Email = "dr.smith@example.com",
                    PhoneNumber = 1234567890,
                    ProfilePicture = "https://example.com/profiles/smith.jpg",
                    SpecialityId = 1
                },
                new User
                {
                    Id = 2,
                    UserName = "dr_jones",
                    Email = "dr.jones@example.com",
                    PhoneNumber = 2345678901,
                    ProfilePicture = "https://example.com/profiles/jones.jpg",
                    SpecialityId = 2
                },
                new User
                {
                    Id = 3,
                    UserName = "dr_williams",
                    Email = "dr.williams@example.com",
                    PhoneNumber = 3456789012,
                    ProfilePicture = "https://example.com/profiles/williams.jpg",
                    SpecialityId = 3
                },
                new User
                {
                    Id = 4,
                    UserName = "dr_brown",
                    Email = "dr.brown@example.com",
                    PhoneNumber = 4567890123,
                    ProfilePicture = "https://example.com/profiles/brown.jpg",
                    SpecialityId = 4
                },
                new User
                {
                    Id = 5,
                    UserName = "dr_taylor",
                    Email = "dr.taylor@example.com",
                    PhoneNumber = 5678901234,
                    ProfilePicture = "https://example.com/profiles/taylor.jpg",
                    SpecialityId = 5
                }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Cardiology CME 2023",
                    Description = "Continuing Medical Education for Cardiology",
                    Date = now.AddDays(30),
                    noOfAttendees = 120,
                    imageUrl = "https://example.com/events/cardio-cme.jpg",
                    eventTypeId = 1,
                    Status = "Upcoming",
                    linkToevent = "https://zoom.us/cardio-cme-2023",
                    eventSpecialityId = 1
                },
                new Event
                {
                    Id = 2,
                    Title = "Pediatric CME Update",
                    Description = "Latest updates in pediatric medicine",
                    Date = now.AddDays(-15),
                    noOfAttendees = 80,
                    imageUrl = "https://example.com/events/ped-cme.jpg",
                    eventTypeId = 1,
                    Status = "Pervious",
                    linkToevent = "",
                    eventSpecialityId = 3
                },
                new Event
                {
                    Id = 3,
                    Title = "Neurology Webinar Series",
                    Description = "Monthly webinars on neurology advancements",
                    Date = now.AddDays(7),
                    noOfAttendees = 75,
                    imageUrl = "https://example.com/events/neuro-webinar.jpg",
                    eventTypeId = 2,
                    Status = "Upcoming",
                    linkToevent = "https://zoom.us/neuro-webinar",
                    eventSpecialityId = 2
                },
                new Event
                {
                    Id = 4,
                    Title = "Oncology Webinar",
                    Description = "Recent advances in cancer treatment",
                    Date = now.AddDays(45),
                    noOfAttendees = 90,
                    imageUrl = "https://example.com/events/onco-webinar.jpg",
                    eventTypeId = 2,
                    Status = "Upcoming",
                    linkToevent = "https://zoom.us/onco-webinar",
                    eventSpecialityId = 4
                },
                new Event
                {
                    Id = 5,
                    Title = "GP Webinar: Annual Updates",
                    Description = "Important updates for general practitioners",
                    Date = now.AddDays(-5),
                    noOfAttendees = 150,
                    imageUrl = "https://example.com/events/gp-webinar.jpg",
                    eventTypeId = 2,
                    Status = "Pervious",
                    linkToevent = "",
                    eventSpecialityId = 5
                }
            );
            var registrationDate = now.AddDays(-20);
            modelBuilder.Entity<UserRegistrationToEvent>().HasData(
                new { UserId = 1, EventId = 1, RegistrationDate = registrationDate.AddDays(1), IsCancelled = false },
                new { UserId = 3, EventId = 2, RegistrationDate = registrationDate.AddDays(2), IsCancelled = false },
                new { UserId = 1, EventId = 2, RegistrationDate = registrationDate.AddDays(3), IsCancelled = true },
                new { UserId = 2, EventId = 3, RegistrationDate = registrationDate.AddDays(4), IsCancelled = false },
                new { UserId = 4, EventId = 4, RegistrationDate = registrationDate.AddDays(5), IsCancelled = false },
                new { UserId = 5, EventId = 5, RegistrationDate = registrationDate.AddDays(6), IsCancelled = false },
                new { UserId = 3, EventId = 5, RegistrationDate = registrationDate.AddDays(7), IsCancelled = true }
            );

            modelBuilder.Entity<ActivityType>().HasData(
                new ActivityType { Id = 1, Type = "Module" },
                new ActivityType { Id = 2, Type = "Activity" }
            );

            modelBuilder.Entity<Activity>().HasData(
                new Activity
                {
                    Id = 1,
                    Title = "Cardio Basics Module",
                    Description = "Introduction to cardiology principles",
                    Date = now.AddDays(25),
                    no = 1,
                    ActivityTypeId = 1,
                    EventId = 1
                },
                new Activity
                {
                    Id = 2,
                    Title = "Pediatric Care Activity",
                    Description = "Hands-on pediatric patient care",
                    Date = now.AddDays(-10),
                    no = 2,
                    ActivityTypeId = 2,
                    EventId = 2
                },
                new Activity
                {
                    Id = 3,
                    Title = "Neuro Module 1",
                    Description = "Fundamentals of neurology",
                    Date = now.AddDays(5),
                    no = 1,
                    ActivityTypeId = 1,
                    EventId = 3
                }
            );

            modelBuilder.Entity<Attachment>().HasData(
                new Attachment
                {
                    Id = 1,
                    FileName = "cardio-module.pdf",
                    FilePath = "/files/cardio-module.pdf",
                    ActivityId = 1
                },
                new Attachment
                {
                    Id = 2,
                    FileName = "pediatric-activity.pdf",
                    FilePath = "/files/pediatric-activity.pdf",
                    ActivityId = 2
                },
                new Attachment
                {
                    Id = 3,
                    FileName = "neuro-module1.pdf",
                    FilePath = "/files/neuro-module1.pdf",
                    ActivityId = 3
                }
            );

            modelBuilder.Entity<ActivitySpeaker>().HasData(
                new { ActivityId = 1, UserId = 1 },
                new { ActivityId = 1, UserId = 2 },
                new { ActivityId = 2, UserId = 3 },
                new { ActivityId = 3, UserId = 2 },
                new { ActivityId = 3, UserId = 4 }
            );
        }


    }
}