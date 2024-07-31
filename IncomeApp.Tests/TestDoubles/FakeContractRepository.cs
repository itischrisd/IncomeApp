using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakeContractRepository : IContractRepository
{
    private IEnumerable<Contract> _contracts = new List<Contract>
    {
        new()
        {
            IdContract = 1,
            StartDate = new DateOnly(2022, 1, 1),
            EndDate = new DateOnly(2022, 12, 31),
            Cost = 1000,
            YearsOfUpdates = 1,
            SoftwareVersion = "1.0",
            IdCustomer = 1,
            IdSoftware = 1
        },
        new()
        {
            IdContract = 2,
            StartDate = new DateOnly(2022, 1, 1),
            EndDate = new DateOnly(2022, 12, 31),
            Cost = 2000,
            YearsOfUpdates = 2,
            SoftwareVersion = "2.0",
            IdCustomer = 2,
            IdSoftware = 2
        },
        new()
        {
            IdContract = 3,
            StartDate = new DateOnly(2022, 1, 1),
            EndDate = new DateOnly(2022, 12, 31),
            Cost = 3000,
            YearsOfUpdates = 3,
            SoftwareVersion = "3.0",
            IdCustomer = 3,
            IdSoftware = 3
        },
        new()
        {
            IdContract = 4,
            StartDate = new DateOnly(2022, 1, 1),
            EndDate = new DateOnly(2022, 12, 31),
            Cost = 4000,
            YearsOfUpdates = 4,
            SoftwareVersion = "4.0",
            IdCustomer = 4,
            IdSoftware = 1
        },
        new()
        {
            IdContract = 5,
            StartDate = new DateOnly(2022, 1, 1),
            EndDate = new DateOnly(2022, 12, 31),
            Cost = 5000,
            YearsOfUpdates = 1,
            SoftwareVersion = "5.0",
            IdCustomer = 5,
            IdSoftware = 2
        },
        new()
        {
            IdContract = 6,
            StartDate = new DateOnly(2022, 1, 1),
            EndDate = new DateOnly(2022, 12, 31),
            Cost = 6000,
            YearsOfUpdates = 2,
            SoftwareVersion = "6.0",
            IdCustomer = 6,
            IdSoftware = 3
        }
    };

    public Task<Contract> GetContractAsync(int idContract, CancellationToken cancellationToken)
    {
        return Task.FromResult(_contracts.FirstOrDefault(c => c.IdContract == idContract));
    }

    public Task<int> AddContractAsync(Contract contract, CancellationToken cancellationToken)
    {
        var id = _contracts.Max(c => c.IdContract) + 1;
        _contracts = _contracts.Append(new Contract
        {
            IdContract = id,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Cost = contract.Cost,
            YearsOfUpdates = contract.YearsOfUpdates,
            SoftwareVersion = contract.SoftwareVersion,
            IdCustomer = contract.IdCustomer,
            IdSoftware = contract.IdSoftware
        });
        return Task.FromResult(id);
    }

    public void DeleteContract(int idContract, CancellationToken cancellationToken)
    {
        _contracts = _contracts.Where(c => c.IdContract != idContract);
    }

    public Task<IEnumerable<Contract>> GetContractsAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_contracts);
    }
}