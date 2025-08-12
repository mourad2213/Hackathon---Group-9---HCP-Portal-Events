using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;

namespace HCP_Portal_Events.DataAccess.Repositories
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookEventAsync(int userId, int eventId)
        {
            var exists = await _context.UserRegistrationToEvents
                .AnyAsync(ue => ue.UserId == userId && ue.EventId == eventId && !ue.IsCancelled);

            if (exists)
                return false;

            var registration = new UserRegistrationToEvent
            {
                UserId = userId,
                EventId = eventId,
                RegistrationDate = DateTime.UtcNow,
                IsCancelled = false
            };

            _context.UserRegistrationToEvents.Add(registration);
            return true;
        }

        public async Task<bool> UnbookEventAsync(int userId, int eventId)
        {
            var registration = await _context.UserRegistrationToEvents
                .FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == eventId && !ue.IsCancelled);

            if (registration == null)
                return false;

            registration.IsCancelled = true;
            return true;
        }

        public async Task<bool> IsUserRegisteredAsync(int userId, int eventId)
        {
            return await _context.UserRegistrationToEvents
                .AnyAsync(ue => ue.UserId == userId && ue.EventId == eventId && !ue.IsCancelled);
        }
    }
}

