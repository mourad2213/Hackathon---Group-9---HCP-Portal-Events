namespace HCP_Portal_Events.Models
{
    public class ActivitySpeaker
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
