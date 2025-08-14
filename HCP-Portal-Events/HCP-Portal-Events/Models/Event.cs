using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HCP_Portal_Events.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int NoOfAttendees { get; set; }
        public string ImageUrl { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public string Status { get; set; }

        public string LinkToEvent { get; set; }

        // Foreign key for Speciality
        public int EventSpecialityId { get; set; }
        public Speciality EventSpeciality { get; set; }

        public ICollection<UserRegistrationToEvent> EventUsers { get; set; }
        public ICollection<Activity> EventActivities { get; set; }

    }
}
