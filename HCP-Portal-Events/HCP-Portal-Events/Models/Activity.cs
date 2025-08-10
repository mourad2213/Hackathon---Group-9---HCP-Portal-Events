namespace HCP_Portal_Events.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }//required
        public string Description { get; set; }
        public DateTime Date { get; set; }//required
        public String Speaker {  get; set; }
        public int no {  get; set; }//required
        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }//required
        public int EventId { get; set; }//required
        public Event Event { get; set; }
    }
}
