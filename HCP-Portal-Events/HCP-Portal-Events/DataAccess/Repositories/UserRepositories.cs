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
            user.ProfilePicture = userUpdateDto.ProfilePicturePath;
            return true;

        }

        public async Task<UserReadDTO> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Speciality)
                .Where(u => u.Id == id)
                .Select(u => new UserReadDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ProfilePicture = u.ProfilePicture,
                    Speciality = u.Speciality.Field // Access the Name property of Speciality
                })
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<EventReadDTO>> GetUserPerviousEvents(int userId) { 
        
            return await _context.UserRegistrationToEvents
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Event)
                    .ThenInclude(e => e.EventType) // Include EventType for mapping
                .Include(ur => ur.Event)
                    .ThenInclude(e => e.EventSpeciality) // Include EventSpeciality for mapping
                .Where(ur => ur.Event.EventCreatedDate < DateTime.Now)
                .Select(ur => new EventReadDTO
                {
                    Id = ur.Event.Id,
                    Title = ur.Event.Title,
                    Description = ur.Event.Description,
                    EventCreatedDate = ur.Event.EventCreatedDate,
                    Status = ur.Event.Status,
                    imageUrl = ur.Event.ImageUrl,
                    EventType = ur.Event.EventType.EventTypeName, // Map EventType name
                    EventField = ur.Event.EventSpeciality.Field // Map Speciality name
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<EventReadDTO>> GetUserUpcomingEvents(int userId)
        {
            return await _context.UserRegistrationToEvents
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Event)
                    .ThenInclude(e => e.EventType)
                .Include(ur => ur.Event)
                    .ThenInclude(e => e.EventSpeciality)
                .Where(ur => ur.Event.EventCreatedDate > DateTime.Now)
                .Select(ur => new EventReadDTO
                {
                    Id = ur.Event.Id,
                    Title = ur.Event.Title,
                    Description = ur.Event.Description,
                    EventCreatedDate = ur.Event.EventCreatedDate,
                    Status = ur.Event.Status,
                    imageUrl = ur.Event.ImageUrl,
                    EventType = ur.Event.EventType.EventTypeName,
                    EventField = ur.Event.EventSpeciality.Field
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<EventReadDTO>> GetUserSpecialityEvents(UserReadDTO user)
        {
            return await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.EventSpeciality)
                .Where(e => e.EventSpeciality.Field == user.Speciality)
                .OrderBy(e => e.EventCreatedDate)
                .Select(e => new EventReadDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    EventCreatedDate = e.EventCreatedDate,
                    Status = e.Status,
                    imageUrl = e.ImageUrl,
                    EventType = e.EventType.EventTypeName,
                    EventField = e.EventSpeciality.Field
                })
                .ToListAsync();
        }

     
        
    }
}
