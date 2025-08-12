using HCP_Portal_Events.Models.DTOs;

namespace HCP_Portal_Events.DataAccess.Interfaces
{
    public interface IEventRepository 
    {
        Task<IEnumerable<EventReadDTO>> GetAllUpcomingEventsAsync();
        Task<IEnumerable<EventReadDTO>> GetAllPreviousEventsAsync();
        /*Task<IEnumerable<EventReadActivitesandAttachmentsDTO>> GetEventAgenda();*/
        public Task<EventReadActivitesandAttachmentsDTO> GetEventWithActivitiesAndAttachments(int id);
        Task<EventReadDTO> GetEventByIdAsync(int id);

    }
}
