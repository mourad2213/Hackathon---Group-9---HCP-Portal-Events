using System.Xml;

namespace HCP_Portal_Events.Models
{
    public class EventStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public ICollection<Event> MyEntities { get; set; } 
    }
}
