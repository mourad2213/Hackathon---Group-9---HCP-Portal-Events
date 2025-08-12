using HCP_Portal_Events.Models;
using HCP_Portal_Events.Models.DTOs;
using System.Collections.Generic;
using static HCP_Portal_Events.Models.DTOs.UserDTO;

namespace HCP_Portal_Events.DataAccess.Interfaces
{
    public interface IUserRepositiories
    {
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);

        Task<IEnumerable<Event>> GetUserPerviousEvents(int userId);
        Task<IEnumerable<Event>> GetUserUpcomingEvents(int userId);
        Task<IEnumerable<Event>> GetUserSpecialityEvents(User user);
    }
}
