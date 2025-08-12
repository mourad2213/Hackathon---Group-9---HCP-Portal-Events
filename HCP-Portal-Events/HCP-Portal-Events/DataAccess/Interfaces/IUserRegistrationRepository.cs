using HCP_Portal_Events.Models;

namespace HCP_Portal_Events.DataAccess.Interfaces
{
    public interface IUserRegistrationRepository
    {
        Task<bool> BookEventAsync(int userId, int eventId);
        Task<bool> UnbookEventAsync(int userId, int eventId);
        Task<bool> IsUserRegisteredAsync(int userId, int eventId);

    }
}
