using HCP_Portal_Events.Models;
using HCP_Portal_Events.Models.DTOs;
using System.Collections.Generic;

namespace HCP_Portal_Events.DataAccess.Interfaces
{
    public interface IUserRepositiories
    {
        Task<UserReadDTO> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);

        Task<IEnumerable<EventReadDTO>> GetUserPerviousEvents(int userId);
        Task<IEnumerable<EventReadDTO>> GetUserUpcomingEvents(int userId);
        Task<IEnumerable<EventReadDTO>> GetUserSpecialityEvents(UserReadDTO user);
    }
}
