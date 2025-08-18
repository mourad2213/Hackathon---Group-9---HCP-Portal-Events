namespace HCP_Portal_Events.Models.DTOs
{
    public class EventReadAttachmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventCreatedDate { get; set; }
        public string imageUrl { get; set; }
        public string EventType { get; set; }
        public string EventField { get; set; }
        public ICollection<Activity> EventActivities { get; set; }

    }
}
