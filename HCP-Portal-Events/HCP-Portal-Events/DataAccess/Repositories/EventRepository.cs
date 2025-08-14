using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
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
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventReadDTO>> GetAllUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.Status == "Upcoming")
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventReadDTO>> GetAllEventsByTypeAsync(string type)
        {
            return await _context.Events
                .Where(e => e.EventType.Type == type)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    Status = e.Status,
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventReadDTO>> GetAllEventsByStatusAsync(string status)
        {
            return await _context.Events
                .Where(e => e.Status == status)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    Status = e.Status,
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
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
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field,

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
                                .ToList(),
                            Speakers = a.ActivitySpeakers
                                .Select(s => new SpeakerDTO
                                {
                                    Id = s.User.Id,
                                    UserName = s.User.UserName,
                                    ProfilePicture = s.User.ProfilePicture,
                                    Speciality = s.User.Speciality.Field
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }


        public async Task<EventReadDTO> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Where(e => e.Id == id)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<EventReadDTO>> SearchEventsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<EventReadDTO>();

            string lowerName = name.ToLower();

            return await _context.Events
                .Where(e => e.Title.ToLower().Contains(lowerName) ||
                            e.Description.ToLower().Contains(lowerName))
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    Status = e.Status,
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

    }
}
