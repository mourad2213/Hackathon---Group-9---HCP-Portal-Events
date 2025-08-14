using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.DataAccess.Reposatiores;
using MyApiProject.Data;

namespace HCP_Portal_Events.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IUserRegistrationRepository UserRegistrationRepository { get; }
        public IEventRepository EventRepository { get; }
        public IUserRepositiories UserRepositiory { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            UserRegistrationRepository = new UserRegistrationRepository(_context);
            EventRepository = new EventRepository(_context);
            UserRepositiory = new UserRepositories(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
