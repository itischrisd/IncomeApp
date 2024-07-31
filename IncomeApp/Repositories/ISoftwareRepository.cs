using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface ISoftwareRepository
{
    Task<Software> GetSoftwareAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Software>> GetSoftwaresAsync(CancellationToken cancellationToken);
}