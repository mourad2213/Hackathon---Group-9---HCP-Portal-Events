namespace HCP_Portal_Events.Models
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string ActivityTypeName { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    }
}