using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HCP_Portal_Events.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int noOfAttendees { get; set; }
        public string imageUrl { get; set; }
        public int eventTypeId { get; set; }
        public EventType eventType { get; set; }
        public string Status { get; set; } 
        /* public int eventStatusId { get; set; }
         public EventStatus eventStatus { get; set; } */
        /*public int eventStatusId { get; set; }
        public EventStatus eventStatus { get; set; } */
        public string linkToevent {  get; set; }
        public int eventSpecialityId { get; set; }
        public Speciality eventSpeciality { get; set; }
        public ICollection<UserRegistrationToEvent> EventUsers { get; set; }
        public ICollection<Activity> EventActivities { get; set; }


    }
}
