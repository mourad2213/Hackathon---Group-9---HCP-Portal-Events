namespace HCP_Portal_Events.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public String Speaker {  get; set; }

        public int no {  get; set; }

        public int ActivityId { get; set; }
        public ActivityType ActivityType { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
