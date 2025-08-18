using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
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
                .Where(e => e.Status == EventStatuses.Previous)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.ImageUrl,
                    Status = e.Status,  
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventReadDTO>> GetAllUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.Status == EventStatuses.Upcoming)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    imageUrl = e.ImageUrl,
                    Status = e.Status,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EventReadDTO>> GetAllEventsByTypeAsync(string type)
        {
            var normalizedType = type.Trim().ToLower();

            return await _context.Events
                .Where(e => e.EventType.Type.Trim().ToLower() == normalizedType)
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
            var normalizedStatus = status.Trim().ToLower();

            return await _context.Events
                .Where(e => e.Status.Trim().ToLower() == normalizedStatus)
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
                    Status = e.Status,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field,

                    EventActivities = e.EventActivities
                        .OrderBy(a => a.DayorModule_No)
                        .ThenBy(a => a.Date)
                        .Select(a => new ActivityWithAttachmentsDTO
                        {
                            Id = a.Id,
                            Title = a.Title,
                            ActivityStartTime = a.Date,
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
                                    SpecialityName = s.User.Speciality.Field
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
                    Status = e.Status,
                    EventType = e.EventType.Type,
                    EventField = e.EventSpeciality.Field
                })
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<EventReadDTO>> SearchEventsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<EventReadDTO>();

            return await _context.Events
                .Where(e => e.Title.ToLower().Contains(name.ToLower()) ||
                            e.Description.ToLower().Contains(name.ToLower()))
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