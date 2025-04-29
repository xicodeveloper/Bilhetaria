public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    int Complete(); // Commit
}