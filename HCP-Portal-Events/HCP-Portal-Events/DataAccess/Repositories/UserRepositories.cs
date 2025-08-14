using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MySqlX.XDevAPI.Common;


namespace HCP_Portal_Events.DataAccess.Repositories
{
    public class UserRepositories : IUserRepositiories
    {
        private readonly ApplicationDbContext _context;
        public UserRepositories(ApplicationDbContext context)
        {
            _context = context;   
        }


        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _context.Users.FindAsync(id);
            user.UserName=userUpdateDto.UserName;
            user.Email = userUpdateDto.Email;
            user.PhoneNumber = userUpdateDto.PhoneNumber;
            user.ProfilePicture = userUpdateDto.ProfilePicture;
            return true;

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Speciality)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public  async Task<IEnumerable<Event>> GetUserPerviousEvents(int userId)
        {
            return await _context.UserRegistrationToEvents
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Event)
            .Where(ur => ur.Event.Date < DateTime.Now)
            .Select(ur => ur.Event)
            .ToListAsync();

        }

        public async Task<IEnumerable<Event>> GetUserUpcomingEvents(int userId)
        {
            return await _context.UserRegistrationToEvents
            .Where(ur => ur.UserId == userId)
            .Include(ur => ur.Event)
            .Where(ur => ur.Event.Date > DateTime.Now)
            .Select(ur => ur.Event)
            .ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetUserSpecialityEvents(User user)
        {
            return await _context.Events
            .Where(e => e.EventSpecialityId == user.SpecialityId)
            .OrderBy(e => e.Date)
            .ToListAsync();
        }


    }
}
