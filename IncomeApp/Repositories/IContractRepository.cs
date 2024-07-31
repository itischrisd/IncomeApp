using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface IContractRepository
{
    Task<Contract> GetContractAsync(int idContract, CancellationToken cancellationToken);
    Task<int> AddContractAsync(Contract contract, CancellationToken cancellationToken);
    void DeleteContract(int idContract, CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetContractsAsync(CancellationToken cancellationToken);
}