namespace HCP_Portal_Events.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRegistrationRepository UserRegistrationRepository { get; }
        IUserRepositiories UserRepositories { get; } 
        IEventRepository EventRepository { get; }
        Task<int> CompleteAsync();
    }
}
