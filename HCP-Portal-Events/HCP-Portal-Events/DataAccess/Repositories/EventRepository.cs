
using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using System.Net.Mail;
using static HCP_Portal_Events.Models.DTOs.ActivityWithAttachmentsDTO;

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

        public async Task<EventReadActivitesandAttachmentsDTO> GetEventWithActivitiesAndAttachments(int id)
        {
            return await _context.Events
                .Where(e => e.Id == id)
                .Select(e => new EventReadActivitesandAttachmentsDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.imageUrl,
                    EventType = e.eventType.Type,
                    EventField = e.eventSpeciality.Field,

                    EventActivities = e.EventActivities
                        .OrderBy(a => a.no)
                        .ThenBy(a => a.Date)
                        .Select(a => new ActivityWithAttachmentsDTO
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Date = a.Date,
                            Description = a.Description,

                            Attachments = a.Attachments
                                .Select(att => new AttachmentDTO
                                {
                                    Id = att.Id,
                                    FileName = att.FileName,
                                    FilePath = att.FilePath
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

       /* public async Task<IEnumerable<EventReadAttachmentDTO>> GetEventAttachments(int id)
        {
            return await _context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventActivities)
                    .ThenInclude(a => a.Attachments) 
                .Select(e => new EventReadAttachmentDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.imageUrl,
                    EventType = e.eventType.Type,
                    EventField = e.eventSpeciality.Field,
                    EventActivities = e.EventActivities
                        .OrderBy(a => a.no)
                        .ThenBy(a => a.Date)
                        .ToList()
                })
                .ToListAsync();
        }*/

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
