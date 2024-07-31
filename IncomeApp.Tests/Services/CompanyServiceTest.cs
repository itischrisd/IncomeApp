using System;
using System.Linq;
using System.Threading.Tasks;
using IncomeApp.DTOs.Request;
using IncomeApp.Exceptions;
using IncomeApp.Services;
using IncomeApp.Tests.TestDoubles;
using JetBrains.Annotations;
using Xunit;

namespace IncomeApp.Tests.Services;

[TestSubject(typeof(CompanyService))]
public class CompanyServiceTest
{
    [Fact]
    public async Task GetCompanyAsync_Should_Return_Company_With_Correct_Id()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);
        const int id = 1;

        var company = await companyService.GetCompanyAsync(id, default);

        Assert.Equal("Company1", company.Name);
        Assert.Equal("Address1", company.Address);
        Assert.Equal("Email1", company.Email);
        Assert.Equal("PhoneNumber1", company.PhoneNumber);
        Assert.Equal("KRS1", company.KRS);
    }

    [Fact]
    public async Task GetCompanyAsync_Should_Throw_RecordNotFoundException_When_Company_Not_Found()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);
        const int id = 3;

        await Assert.ThrowsAsync<RecordNotFoundException>(() => companyService.GetCompanyAsync(id, default));
    }

    [Fact]
    public async Task GetCompaniesAsync_Should_Return_All_Companies()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);

        var companies = await companyService.GetCompaniesAsync(default);

        Assert.Equal(2, companies.Count());
    }

    [Fact]
    public async Task AddCompanyAsync_Should_Add_Company()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);
        var company = new CompanyCreateDTO
        {
            Name = "Company3",
            Address = "Address3",
            Email = "Email3",
            PhoneNumber = "PhoneNumber3",
            KRS = "KRS3"
        };

        var id = await companyService.AddCompanyAsync(company, default);

        Assert.Equal(6, id);
        Assert.Equal(3, (await companyRepository.GetCompaniesAsync(default)).Count());
    }

    [Fact]
    public async Task UpdateCompanyAsync_Should_Update_Company()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);
        var company = new CompanyUpdateDTO
        {
            Name = "Company3",
            Address = "Address3",
            Email = "Email3",
            PhoneNumber = "PhoneNumber3"
        };
        const int id = 1;

        await companyService.UpdateCompanyAsync(id, company, default);

        var updatedCompany = await companyRepository.GetCompanyAsync(id, default);
        Assert.Equal("Company3", updatedCompany.Name);
        Assert.Equal("Address3", updatedCompany.Address);
        Assert.Equal("Email3", updatedCompany.Email);
        Assert.Equal("PhoneNumber3", updatedCompany.PhoneNumber);
    }

    [Fact]
    public async Task UpdateCompanyAsync_Should_Throw_Exception_When_Company_Not_Found()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);
        var company = new CompanyUpdateDTO
        {
            Name = "Company3",
            Address = "Address3",
            Email = "Email3",
            PhoneNumber = "PhoneNumber3"
        };
        const int id = 3;

        await Assert.ThrowsAsync<Exception>(() => companyService.UpdateCompanyAsync(id, company, default));
    }

    [Fact]
    public async Task DeleteCompanyAsync_Should_Delete_Company()
    {
        var companyRepository = new FakeCompanyRepository();
        var unitOfWork = new FakeUnitOfWork();
        var companyService = new CompanyService(companyRepository, unitOfWork);
        const int id = 1;

        await companyService.DeleteCompanyAsync(id, default);

        Assert.Single(await companyRepository.GetCompaniesAsync(default));
    }
}