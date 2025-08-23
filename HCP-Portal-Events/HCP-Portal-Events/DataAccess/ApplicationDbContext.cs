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
            // Fixed reference date
            var baseDate = new DateTime(2025, 01, 01, 9, 0, 0);

            // Specialities
            modelBuilder.Entity<Speciality>().HasData(
                new Speciality { Id = 1, Field = "Cardiology" },
                new Speciality { Id = 2, Field = "Neurology" },
                new Speciality { Id = 3, Field = "Pediatrics" },
                new Speciality { Id = 4, Field = "Oncology" },
                new Speciality { Id = 5, Field = "General Practice" }
            );

            // Event Types
            modelBuilder.Entity<EventType>().HasData(
                new EventType { Id = 1, EventTypeName = "CME" },
                new EventType { Id = 2, EventTypeName = "Webinar" }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                 new User { Id = 1, UserName = "dr_smith", Email = "dr.smith@example.com", PhoneNumber = 1234567890, ProfilePicture = "/images/users/download.webp", SpecialityId = 1 },
                 new User { Id = 2, UserName = "dr_jones", Email = "dr.jones@example.com", PhoneNumber = 2345678901, ProfilePicture = "//images/users/download.webp", SpecialityId = 2 },
                 new User { Id = 3, UserName = "dr_williams", Email = "dr.williams@example.com", PhoneNumber = 3456789012, ProfilePicture = "/images/users/download.webp", SpecialityId = 3 },
                 new User { Id = 4, UserName = "dr_brown", Email = "dr.brown@example.com", PhoneNumber = 4567890123, ProfilePicture = "/images/users/download.webp", SpecialityId = 4 },
                 new User { Id = 5, UserName = "dr_taylor", Email = "dr.taylor@example.com", PhoneNumber = 5678901234, ProfilePicture = "/images/users/download.webp", SpecialityId = 5 }
             );


            // Events
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Title = "Cardiology CME 2023", Description = "Continuing Medical Education for Cardiology", EventCreatedDate = baseDate.AddDays(300), NoOfAttendees = 120, ImageUrl = "/images/events/OIP (1).jpeg", EventTypeId = 1, Status = "Upcoming", LinkToEvent = "https://zoom.us/cardio-cme-2023", EventSpecialityId = 1 },
                new Event { Id = 2, Title = "Pediatric CME Update", Description = "Latest updates in pediatric medicine", EventCreatedDate = baseDate.AddDays(0), NoOfAttendees = 80, ImageUrl = "/images/events/OIP (1).webp", EventTypeId = 1, Status = "Previous", LinkToEvent = "", EventSpecialityId = 1 },
                new Event { Id = 3, Title = "Neurology Webinar Series", Description = "Monthly webinars on neurology advancements", EventCreatedDate = baseDate.AddDays(301), NoOfAttendees = 75, ImageUrl = "/images/events/OIP (2).webp", EventTypeId = 2, Status = "Upcoming", LinkToEvent = "https://zoom.us/neuro-webinar", EventSpecialityId = 2 },
                new Event { Id = 4, Title = "Oncology Webinar", Description = "Recent advances in cancer treatment", EventCreatedDate = baseDate.AddDays(45), NoOfAttendees = 90, ImageUrl = "/images/events/OIP.jpeg", EventTypeId = 2, Status = "Previous", LinkToEvent = "https://zoom.us/onco-webinar", EventSpecialityId = 4 },
                new Event { Id = 5, Title = "GP Webinar: Annual Updates", Description = "Important updates for general practitioners", EventCreatedDate = baseDate.AddDays(-5), NoOfAttendees = 150, ImageUrl = "/images/events/OIP.webp", EventTypeId = 2, Status = "Previous", LinkToEvent = "", EventSpecialityId = 5 },
                new Event { Id = 6, Title = "Cardiology Innovations 2025", Description = "Exploring the latest advancements in cardiology treatments and technology", EventCreatedDate = baseDate.AddDays(500), NoOfAttendees = 150, ImageUrl = "/images/events/AdobeStock_65704664-scaled.jpeg", EventTypeId = 1, Status = "Upcoming", LinkToEvent = "https://zoom.us/cardiology-innovations-2025", EventSpecialityId = 1 }
                /*new Event { Id = 7, Title = "Neurology Advances 2025", Description = "Conference on cutting-edge research in neurology and brain health", EventCreatedDate = baseDate.AddDays(550), NoOfAttendees = 180, ImageUrl = "/images/events/Why-Chiropractic-EMR-Software-Is-Becoming-Widely-Used-In-Chiro-Offices-1.jpg", EventTypeId = 1, Status = "Upcoming", LinkToEvent = "https://zoom.us/neurology-advances-2025", EventSpecialityId = 3}*/
            );


            // Registrations
            var registrationBase = baseDate.AddDays(-20);
            modelBuilder.Entity<UserRegistrationToEvent>().HasData(
                new { UserId = 1, EventId = 1, RegistrationDate = registrationBase.AddDays(1), IsCancelled = false },
                new { UserId = 3, EventId = 2, RegistrationDate = registrationBase.AddDays(2), IsCancelled = false },
                new { UserId = 1, EventId = 2, RegistrationDate = registrationBase.AddDays(3), IsCancelled = true },
                new { UserId = 2, EventId = 3, RegistrationDate = registrationBase.AddDays(4), IsCancelled = false },
                new { UserId = 4, EventId = 4, RegistrationDate = registrationBase.AddDays(5), IsCancelled = false },
                new { UserId = 5, EventId = 5, RegistrationDate = registrationBase.AddDays(6), IsCancelled = false },
                new { UserId = 3, EventId = 5, RegistrationDate = registrationBase.AddDays(7), IsCancelled = true }
            );

            // Activity Types
            modelBuilder.Entity<ActivityType>().HasData(
                new ActivityType { Id = 1, ActivityTypeName = "Module" },
                new ActivityType { Id = 2, ActivityTypeName = "Activity" }
            );

            // Activities (at least 2 per Event, sequential numbers)
            modelBuilder.Entity<Activity>().HasData(
                new Activity { Id = 1, Title = "Cardio Basics Module", Description = "Introduction to cardiology principles", Date = baseDate.AddDays(25), DayorModule_No = 1, ActivityTypeId = 1, EventId = 1 },
                new Activity { Id = 2, Title = "Advanced Cardio Module", Description = "In-depth cardiology practices", Date = baseDate.AddDays(26), DayorModule_No = 2, ActivityTypeId = 1, EventId = 1 },

                new Activity { Id = 3, Title = "Pediatric Care Activity", Description = "Hands-on pediatric patient care", Date = baseDate.AddDays(-10), DayorModule_No = 1, ActivityTypeId = 2, EventId = 2 },
                new Activity { Id = 4, Title = "Pediatric Research Module", Description = "Latest research findings in pediatrics", Date = baseDate.AddDays(-9), DayorModule_No = 2, ActivityTypeId = 1, EventId = 2 },

                new Activity { Id = 5, Title = "Neuro Module 1", Description = "Fundamentals of neurology", Date = baseDate.AddDays(5), DayorModule_No = 1, ActivityTypeId = 1, EventId = 3 },
                new Activity { Id = 6, Title = "Neuro Module 2", Description = "Advanced neurology topics", Date = baseDate.AddDays(6), DayorModule_No = 2, ActivityTypeId = 1, EventId = 3 },

                new Activity { Id = 7, Title = "Oncology Module 1", Description = "Cancer biology and basics", Date = baseDate.AddDays(46), DayorModule_No = 1, ActivityTypeId = 1, EventId = 4 },
                new Activity { Id = 8, Title = "Oncology Module 2", Description = "Therapeutic strategies in oncology", Date = baseDate.AddDays(47), DayorModule_No = 2, ActivityTypeId = 1, EventId = 4 },

                new Activity { Id = 9, Title = "GP Update Activity 1", Description = "Annual GP best practices", Date = baseDate.AddDays(-4), DayorModule_No = 1, ActivityTypeId = 2, EventId = 5 },
                new Activity { Id = 10, Title = "GP Update Activity 2", Description = "Case studies for general practitioners", Date = baseDate.AddDays(-3), DayorModule_No = 2, ActivityTypeId = 2, EventId = 5 }
            );

            // Attachments (1 per activity)
            modelBuilder.Entity<Attachment>().HasData(
                new Attachment { Id = 1, FileName = "cardio-module1.pdf", FilePath = "/files/cardio-module1.pdf", ActivityId = 1 },
                new Attachment { Id = 2, FileName = "cardio-module2.pdf", FilePath = "/files/cardio-module2.pdf", ActivityId = 2 },
                new Attachment { Id = 3, FileName = "pediatric-activity.pdf", FilePath = "/files/pediatric-activity.pdf", ActivityId = 3 },
                new Attachment { Id = 4, FileName = "pediatric-research.pdf", FilePath = "/files/pediatric-research.pdf", ActivityId = 4 },
                new Attachment { Id = 5, FileName = "neuro-module1.pdf", FilePath = "/files/neuro-module1.pdf", ActivityId = 5 },
                new Attachment { Id = 6, FileName = "neuro-module2.pdf", FilePath = "/files/neuro-module2.pdf", ActivityId = 6 },
                new Attachment { Id = 7, FileName = "oncology-module1.pdf", FilePath = "/files/oncology-module1.pdf", ActivityId = 7 },
                new Attachment { Id = 8, FileName = "oncology-module2.pdf", FilePath = "/files/oncology-module2.pdf", ActivityId = 8 },
                new Attachment { Id = 9, FileName = "gp-activity1.pdf", FilePath = "/files/gp-activity1.pdf", ActivityId = 9 },
                new Attachment { Id = 10, FileName = "gp-activity2.pdf", FilePath = "/files/gp-activity2.pdf", ActivityId = 10 }
            );

            // Activity Speakers (at least one per activity)
            modelBuilder.Entity<ActivitySpeaker>().HasData(
                new { ActivityId = 1, UserId = 1 },
                new { ActivityId = 2, UserId = 2 },
                new { ActivityId = 3, UserId = 3 },
                new { ActivityId = 4, UserId = 1 },
                new { ActivityId = 5, UserId = 2 },
                new { ActivityId = 6, UserId = 4 },
                new { ActivityId = 7, UserId = 4 },
                new { ActivityId = 8, UserId = 5 },
                new { ActivityId = 9, UserId = 5 },
                new { ActivityId = 10, UserId = 3 }
            );
        }



    }
}