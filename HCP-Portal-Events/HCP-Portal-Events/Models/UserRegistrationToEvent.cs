namespace HCP_Portal_Events.Models
{
    public class UserRegistrationToEvent
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsCancelled { get; set; } 
    }
}
