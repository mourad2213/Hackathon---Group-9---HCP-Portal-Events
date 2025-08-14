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
            // Use fixed reference dates (no DateTime.Now)
            var baseDate = new DateTime(2025, 01, 01, 9, 0, 0);

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
                    Date = baseDate.AddDays(300), // 2025-01-31
                    NoOfAttendees = 120,
                    ImageUrl = "https://example.com/events/cardio-cme.jpg",
                    EventTypeId = 1,
                    Status = "Upcoming",
                    LinkToEvent = "https://zoom.us/cardio-cme-2023",
                    EventSpecialityId = 1
                },
                new Event
                {
                    Id = 2,
                    Title = "Pediatric CME Update",
                    Description = "Latest updates in pediatric medicine",
                    Date = baseDate.AddDays(-15), // 2024-12-17
                    NoOfAttendees = 80,
                    ImageUrl = "https://example.com/events/ped-cme.jpg",
                    EventTypeId = 1,
                    Status = "Previous",
                    LinkToEvent = "",
                    EventSpecialityId = 3
                },
                new Event
                {
                    Id = 3,
                    Title = "Neurology Webinar Series",
                    Description = "Monthly webinars on neurology advancements",
                    Date = baseDate.AddDays(7), // 2025-01-08
                    NoOfAttendees = 75,
                    ImageUrl = "https://example.com/events/neuro-webinar.jpg",
                    EventTypeId = 2,
                    Status = "Upcoming",
                    LinkToEvent = "https://zoom.us/neuro-webinar",
                    EventSpecialityId = 2
                },
                new Event
                {
                    Id = 4,
                    Title = "Oncology Webinar",
                    Description = "Recent advances in cancer treatment",
                    Date = baseDate.AddDays(45), // 2025-02-15
                    NoOfAttendees = 90,
                    ImageUrl = "https://example.com/events/onco-webinar.jpg",
                    EventTypeId = 2,
                    Status = "Upcoming",
                    LinkToEvent = "https://zoom.us/onco-webinar",
                    EventSpecialityId = 4
                },
                new Event
                {
                    Id = 5,
                    Title = "GP Webinar: Annual Updates",
                    Description = "Important updates for general practitioners",
                    Date = baseDate.AddDays(-5), // 2024-12-27
                    NoOfAttendees = 150,
                    ImageUrl = "https://example.com/events/gp-webinar.jpg",
                    EventTypeId = 2,
                    Status = "Previous",
                    LinkToEvent = "",
                    EventSpecialityId = 5
                }
            );

            var registrationBase = baseDate.AddDays(-20); // 2024-12-12
            modelBuilder.Entity<UserRegistrationToEvent>().HasData(
                new { UserId = 1, EventId = 1, RegistrationDate = registrationBase.AddDays(1), IsCancelled = false },
                new { UserId = 3, EventId = 2, RegistrationDate = registrationBase.AddDays(2), IsCancelled = false },
                new { UserId = 1, EventId = 2, RegistrationDate = registrationBase.AddDays(3), IsCancelled = true },
                new { UserId = 2, EventId = 3, RegistrationDate = registrationBase.AddDays(4), IsCancelled = false },
                new { UserId = 4, EventId = 4, RegistrationDate = registrationBase.AddDays(5), IsCancelled = false },
                new { UserId = 5, EventId = 5, RegistrationDate = registrationBase.AddDays(6), IsCancelled = false },
                new { UserId = 3, EventId = 5, RegistrationDate = registrationBase.AddDays(7), IsCancelled = true }
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
                    Date = baseDate.AddDays(25), // 2025-01-26
                    no = 1,
                    ActivityTypeId = 1,
                    EventId = 1
                },
                new Activity
                {
                    Id = 2,
                    Title = "Pediatric Care Activity",
                    Description = "Hands-on pediatric patient care",
                    Date = baseDate.AddDays(-10), // 2024-12-22
                    no = 2,
                    ActivityTypeId = 2,
                    EventId = 2
                },
                new Activity
                {
                    Id = 3,
                    Title = "Neuro Module 1",
                    Description = "Fundamentals of neurology",
                    Date = baseDate.AddDays(5), // 2025-01-06
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