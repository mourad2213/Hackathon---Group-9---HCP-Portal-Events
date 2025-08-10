using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HCP_Portal_Events.Models
{
    public class EventField
    {
        public int Id { get; set; }
        public string Field { get; set; } 

        public ICollection<Event> eventFields { get; set; }
    }
}
