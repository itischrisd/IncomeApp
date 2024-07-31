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

[TestSubject(typeof(ContractService))]
public class ContractServiceTest
{
    [Fact]
    public async Task AddContractAsync_Should_Add_Valid_Contract()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 5),
            YearsOfUpdates = 1,
            IdCustomer = 1,
            IdSoftware = 1
        };

        var id = await contractService.AddContractAsync(contract, default);

        Assert.Equal(7, id);

        Assert.Equal(7, (await contractRepository.GetContractsAsync(default)).Count());

        var newContract = (await contractRepository.GetContractsAsync(default)).Last();

        Assert.Equal(7, newContract.IdContract);
        Assert.Equal(new DateOnly(2023, 1, 1), newContract.StartDate);
        Assert.Equal(new DateOnly(2023, 1, 5), newContract.EndDate);
        Assert.Equal(100, newContract.Cost);
        Assert.Equal(1, newContract.YearsOfUpdates);
        Assert.Equal("16.11.7", newContract.SoftwareVersion);
        Assert.Equal(1, newContract.IdCustomer);
        Assert.Equal(1, newContract.IdSoftware);
    }

    [Fact]
    public async Task AddContractAsync_Should_Throw_If_StartDate_Is_Greater_Than_EndDate()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 5),
            EndDate = new DateOnly(2023, 1, 1),
            YearsOfUpdates = 1,
            IdCustomer = 1,
            IdSoftware = 1
        };

        await Assert.ThrowsAsync<DomainException>(() => contractService.AddContractAsync(contract, default));
    }

    [Fact]
    public async Task AddContractAsync_Should_Throw_If_EndDate_One_Day_After_StartDate()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 2),
            YearsOfUpdates = 1,
            IdCustomer = 1,
            IdSoftware = 1
        };

        await Assert.ThrowsAsync<DomainException>(() => contractService.AddContractAsync(contract, default));
    }

    [Fact]
    public async Task AddContractAsync_Should_Throw_If_Customer_Not_Found()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 5),
            YearsOfUpdates = 1,
            IdCustomer = 10,
            IdSoftware = 1
        };

        await Assert.ThrowsAsync<DomainException>(() => contractService.AddContractAsync(contract, default));
    }

    [Fact]
    public async Task AddContractAsync_Should_Throw_If_Software_Not_Found()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 5),
            YearsOfUpdates = 1,
            IdCustomer = 1,
            IdSoftware = 10
        };

        await Assert.ThrowsAsync<DomainException>(() => contractService.AddContractAsync(contract, default));
    }

    [Fact]
    public async Task AddContractAsync_Should_Throw_If_Customer_Already_Has_Active_Contract_For_Software()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 5),
            YearsOfUpdates = 4,
            IdCustomer = 5,
            IdSoftware = 1
        };

        await Assert.ThrowsAsync<DomainException>(() => contractService.AddContractAsync(contract, default));
    }

    [Fact]
    public async Task AddContractAsync_Should_Add_Contract_With_Discount_If_Discount_Is_Active()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 5),
            YearsOfUpdates = 1,
            IdCustomer = 1,
            IdSoftware = 3
        };

        var id = await contractService.AddContractAsync(contract, default);

        Assert.Equal(7, id);

        Assert.Equal(7, (await contractRepository.GetContractsAsync(default)).Count());

        var newContract = (await contractRepository.GetContractsAsync(default)).Last();

        Assert.Equal(7, newContract.IdContract);
        Assert.Equal(new DateOnly(2023, 1, 1), newContract.StartDate);
        Assert.Equal(new DateOnly(2023, 1, 5), newContract.EndDate);
        Assert.Equal(225, newContract.Cost);
        Assert.Equal(1, newContract.YearsOfUpdates);
        Assert.Equal("22.5.1", newContract.SoftwareVersion);
        Assert.Equal(1, newContract.IdCustomer);
        Assert.Equal(3, newContract.IdSoftware);
    }

    [Fact]
    public async Task AddContractAsync_Should_Add_Contract_With_Discount_If_Customer_Has_Signed_Contract()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        var contract = new ContractCreateDTO
        {
            StartDate = new DateOnly(2023, 1, 1),
            EndDate = new DateOnly(2023, 1, 5),
            YearsOfUpdates = 1,
            IdCustomer = 3,
            IdSoftware = 1
        };

        var id = await contractService.AddContractAsync(contract, default);

        Assert.Equal(7, id);

        Assert.Equal(7, (await contractRepository.GetContractsAsync(default)).Count());

        var newContract = (await contractRepository.GetContractsAsync(default)).Last();

        Assert.Equal(7, newContract.IdContract);
        Assert.Equal(new DateOnly(2023, 1, 1), newContract.StartDate);
        Assert.Equal(new DateOnly(2023, 1, 5), newContract.EndDate);
        Assert.Equal(95, newContract.Cost);
        Assert.Equal(1, newContract.YearsOfUpdates);
        Assert.Equal("16.11.7", newContract.SoftwareVersion);
        Assert.Equal(3, newContract.IdCustomer);
        Assert.Equal(1, newContract.IdSoftware);
    }

    [Fact]
    public async Task DeleteContractAsync_Should_Delete_Contract()
    {
        var contractRepository = new FakeContractRepository();
        var personRepository = new FakePersonRepository();
        var companyRepository = new FakeCompanyRepository();
        var customerRepository = new FakeCustomerRepository(personRepository, companyRepository);
        var softwareRepository = new FakeSoftwareRepository();
        var unitOfWork = new FakeUnitOfWork();
        var contractService =
            new ContractService(contractRepository, customerRepository, softwareRepository, unitOfWork);

        const int id = 1;

        await contractService.DeleteContractAsync(id, default);

        Assert.Equal(5, (await contractRepository.GetContractsAsync(default)).Count());
    }
}