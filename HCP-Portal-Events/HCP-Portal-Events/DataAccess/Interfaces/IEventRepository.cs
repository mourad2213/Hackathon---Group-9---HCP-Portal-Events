using HCP_Portal_Events.Models.DTOs;

namespace HCP_Portal_Events.DataAccess.Interfaces
{
    public interface IEventRepository 
    {
        Task<IEnumerable<EventReadDTO>> GetAllUpcomingEventsAsync();
        Task<IEnumerable<EventReadDTO>> GetAllPreviousEventsAsync();
        Task<EventReadActivitesandAttachmentsDTO> GetEventWithActivitiesAndAttachments(int id);
        Task<EventReadDTO> GetEventByIdAsync(int id);

        Task<IEnumerable<EventReadDTO>> GetAllEventsByTypeAsync(String Type);
        Task<IEnumerable<EventReadDTO>> GetAllEventsByStatusAsync(String Status );
        Task<IEnumerable<EventReadDTO>> SearchEventsByNameAsync(string name);
    }
}
