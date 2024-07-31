namespace IncomeApp.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task InitializeAsync(CancellationToken cancellationToken);
    Task<int> CommitAsync(CancellationToken cancellationToken);
}