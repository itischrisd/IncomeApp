using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface ICompanyRepository
{
    Task<Company> GetCompanyAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken);
    Task<int> AddCompanyAsync(Company company, CancellationToken cancellationToken);
    Task UpdateCompanyAsync(Company company, CancellationToken cancellationToken);
    Task DeleteCompanyAsync(int id, CancellationToken cancellationToken);
}