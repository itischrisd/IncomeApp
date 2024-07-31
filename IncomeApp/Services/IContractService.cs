using IncomeApp.DTOs.Request;

namespace IncomeApp.Services;

public interface IContractService
{
    Task<int> AddContractAsync(ContractCreateDTO contract, CancellationToken cancellationToken);
    Task<int> DeleteContractAsync(int id, CancellationToken cancellationToken);
}