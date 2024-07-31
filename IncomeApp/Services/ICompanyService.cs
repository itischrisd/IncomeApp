using IncomeApp.DTOs.Request;
using IncomeApp.DTOs.Response;

namespace IncomeApp.Services;

public interface ICompanyService
{
    Task<CompanyDTO> GetCompanyAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CompanyDTO>> GetCompaniesAsync(CancellationToken cancellationToken);
    Task<int> AddCompanyAsync(CompanyCreateDTO company, CancellationToken cancellationToken);
    Task<int> UpdateCompanyAsync(int id, CompanyUpdateDTO company, CancellationToken cancellationToken);
}