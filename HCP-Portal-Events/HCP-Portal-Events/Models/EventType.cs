using System.Xml;

namespace HCP_Portal_Events.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string EventTypeName { get; set; }

        public ICollection<Event> Events { get; set; } 
    }
}
