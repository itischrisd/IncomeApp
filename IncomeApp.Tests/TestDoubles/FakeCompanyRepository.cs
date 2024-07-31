using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakeCompanyRepository : ICompanyRepository
{
    private IEnumerable<Company> _companies = new List<Company>
    {
        new()
        {
            IdCustomer = 1,
            Name = "Company1",
            Address = "Address1",
            Email = "Email1",
            PhoneNumber = "PhoneNumber1",
            KRS = "KRS1"
        },
        new()
        {
            IdCustomer = 5,
            Name = "Company2",
            Address = "Address2",
            Email = "Email2",
            PhoneNumber = "PhoneNumber2",
            KRS = "KRS2",
            Contracts = new List<Contract>
            {
                new()
                {
                    IdContract = 1,
                    StartDate = new DateOnly(2024, 1, 1),
                    EndDate = new DateOnly(2024, 1, 15),
                    Cost = 1000,
                    YearsOfUpdates = 4,
                    SoftwareVersion = "1.0",
                    IdCustomer = 2,
                    IdSoftware = 1
                }
            }
        }
    };

    public Task<Company> GetCompanyAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_companies.FirstOrDefault(c => c.IdCustomer == id));
    }

    public Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_companies);
    }

    public Task<int> AddCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        var id = _companies.Max(c => c.IdCustomer) + 1;
        _companies = _companies.Append(new Company
        {
            IdCustomer = id,
            Name = company.Name,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            KRS = company.KRS
        });
        return Task.FromResult(id);
    }

    public Task UpdateCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        var companyToUpdate = _companies.FirstOrDefault(c => c.IdCustomer == company.IdCustomer);
        if (companyToUpdate == null) throw new Exception("Company not found");

        _companies = _companies.Select(c => c.IdCustomer == company.IdCustomer ? company : c);
        return Task.CompletedTask;
    }

    public Task DeleteCompanyAsync(int id, CancellationToken cancellationToken)
    {
        var companyToDelete = _companies.FirstOrDefault(c => c.IdCustomer == id);
        if (companyToDelete == null) throw new Exception("Company not found");

        _companies = _companies.Where(c => c.IdCustomer != id);
        return Task.CompletedTask;
    }
}