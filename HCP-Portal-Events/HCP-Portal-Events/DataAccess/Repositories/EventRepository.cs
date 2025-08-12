using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;

namespace HCP_Portal_Events.DataAccess.Reposatiores
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EventReadDTO>> GetAllPreviousEventsAsync()
        {
            return await _context.Events
                .Where(e => e.Status == "Previous") 
                .Select(e => new EventReadDTO
                        {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.imageUrl, 
                    EventType = e.eventType.Type,
                    EventField = e.eventSpeciality.Field
                })
                .ToListAsync();
        }

        public  async Task<IEnumerable<EventReadDTO>> GetAllUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.Status == "Upcoming") 
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.imageUrl,
                    EventType = e.eventType.Type,   
                    EventField = e.eventSpeciality.Field 
                })
                .ToListAsync();
        }

        public async Task<EventReadDTO?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Where(e => e.Id == id)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.imageUrl,
                    EventType = e.eventType.Type,   
                    EventField = e.eventSpeciality.Field 
                })
                .FirstOrDefaultAsync();
        }
    }
}
