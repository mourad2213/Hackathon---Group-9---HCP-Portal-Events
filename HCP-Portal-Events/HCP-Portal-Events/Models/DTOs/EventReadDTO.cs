namespace HCP_Portal_Events.Models.DTOs
{
    public class EventReadDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string imageUrl { get; set; }
        public string EventType { get; set;}
        public string EventField { get; set;}
    }
}
