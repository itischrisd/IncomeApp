using IncomeApp.Exceptions;
using IncomeApp.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace IncomeApp.Repositories;

public class UnitOfWork(Context context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        if (_transaction == null) throw new NotInitalizedException();

        try
        {
            var affectedRowCount = await context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            return affectedRowCount;
        }
        catch
        {
            await _transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public void Dispose()
    {
        context.Dispose();
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        if (_transaction == null) return;
        await _transaction.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}