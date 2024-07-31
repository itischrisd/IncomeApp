using IncomeApp.DTOs.Request;
using IncomeApp.DTOs.Response;
using IncomeApp.Exceptions;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork) : ICompanyService
{
    public async Task<CompanyDTO> GetCompanyAsync(int id, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetCompanyAsync(id, cancellationToken);
        if (company == null) throw new RecordNotFoundException("Company not found");
        return new CompanyDTO
        {
            IdCompany = company.IdCustomer,
            Name = company.Name,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            KRS = company.KRS
        };
    }

    public async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        return (await companyRepository.GetCompaniesAsync(cancellationToken))
            .Select(c => new CompanyDTO
            {
                IdCompany = c.IdCustomer,
                Name = c.Name,
                Address = c.Address,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                KRS = c.KRS
            });
    }

    public async Task<int> AddCompanyAsync(CompanyCreateDTO company, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        var id = await companyRepository.AddCompanyAsync(new Company
        {
            Name = company.Name,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            KRS = company.KRS
        }, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return id;
    }

    public async Task<int> UpdateCompanyAsync(int id, CompanyUpdateDTO company, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        await companyRepository.UpdateCompanyAsync(new Company
        {
            IdCustomer = id,
            Name = company.Name,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber
        }, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task<int> DeleteCompanyAsync(int id, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        await companyRepository.DeleteCompanyAsync(id, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}