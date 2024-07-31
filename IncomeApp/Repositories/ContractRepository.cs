using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class ContractRepository(Context context) : IContractRepository
{
    public async Task<Contract> GetContractAsync(int idContract, CancellationToken cancellationToken)
    {
        return (await context.Contracts
            .Include(c => c.Payments)
            .FirstOrDefaultAsync(c => c.IdContract == idContract, cancellationToken))!;
    }

    public async Task<int> AddContractAsync(Contract contract, CancellationToken cancellationToken)
    {
        await context.Contracts.AddAsync(contract, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return contract.IdContract;
    }

    public void DeleteContract(int idContract, CancellationToken cancellationToken)
    {
        context.Contracts.Remove(new Contract { IdContract = idContract });
    }

    public async Task<IEnumerable<Contract>> GetContractsAsync(CancellationToken cancellationToken)
    {
        return await context.Contracts.ToListAsync(cancellationToken);
    }
}