public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    int Complete(); // Commit
    IOrderRepository Order { get; }
    Task<int> CompleteAsync();
}