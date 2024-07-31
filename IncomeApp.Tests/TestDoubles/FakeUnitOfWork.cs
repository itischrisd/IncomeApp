using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakeUnitOfWork : IUnitOfWork
{
    public void Dispose()
    {
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}