using IncomeApp.DTOs.Request;
using IncomeApp.Exceptions;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class ContractService(
    IContractRepository contractRepository,
    ICustomerRepository customerRepository,
    ISoftwareRepository softwareRepository,
    IUnitOfWork unitOfWork) : IContractService
{
    public async Task<int> AddContractAsync(ContractCreateDTO contract, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);

        EnsureTimePeriodIsValid(contract.StartDate, contract.EndDate);

        var software = await GetRelatedSoftwareAndDiscounts(contract, cancellationToken);
        var customer = await GetRelatedCustomer(contract, cancellationToken);

        EnsureThereIsNoActiveContractForSoftware(customer, software);

        var softwareDiscount = FindBestPossibleActiveDiscount(software.Discounts);
        var customerDiscount = CheckIfEligibleForCustomerDiscount(customer);
        var totalDiscount = softwareDiscount + customerDiscount;
        var totalCost = (1000 * (contract.YearsOfUpdates - 1) + software.YearlyCost * contract.YearsOfUpdates) *
                        (1 - (decimal)totalDiscount / 100);

        var id = await contractRepository.AddContractAsync(new Contract
        {
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Cost = totalCost,
            YearsOfUpdates = contract.YearsOfUpdates,
            SoftwareVersion = software.CurrentVersion,
            IdCustomer = contract.IdCustomer,
            IdSoftware = contract.IdSoftware
        }, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return id;
    }

    public async Task<int> DeleteContractAsync(int id, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        contractRepository.DeleteContract(id, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }

    private static void EnsureThereIsNoActiveContractForSoftware(Customer customer, Software software)
    {
        if (customer.Contracts.Any(c => c.IdSoftware == software.IdSoftware &&
                                        DateOnly.FromDateTime(DateTime.Now) <= c.StartDate.AddYears(c.YearsOfUpdates)))
            throw new DomainException("Customer already has an active contract for this software");
    }

    private async Task<Customer> GetRelatedCustomer(ContractCreateDTO contract, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetCustomerAsync(contract.IdCustomer, cancellationToken);
        if (customer == null)
            throw new DomainException("Customer not found");
        return customer;
    }

    private static int CheckIfEligibleForCustomerDiscount(Customer customer)
    {
        return customer.Contracts.Any(c => c.IsSigned) ? 5 : 0;
    }

    private static int FindBestPossibleActiveDiscount(IEnumerable<Discount> discounts)
    {
        return discounts.Where(d => d.StartDate <= DateOnly.FromDateTime(DateTime.Now) &&
                                    d.EndDate >= DateOnly.FromDateTime(DateTime.Now))
            .MaxBy(d => d.Percentage)
            ?.Percentage ?? 0;
    }

    private async Task<Software> GetRelatedSoftwareAndDiscounts(ContractCreateDTO contract,
        CancellationToken cancellationToken)
    {
        var software = await softwareRepository.GetSoftwareAsync(contract.IdSoftware, cancellationToken);
        if (software == null)
            throw new DomainException("Software not found");
        return software;
    }

    private static void EnsureTimePeriodIsValid(DateOnly contractStartDate, DateOnly contractEndDate)
    {
        if (contractStartDate >= contractEndDate)
            throw new DomainException("Contract start date must be before contract end date");
        var daysDifference = contractEndDate.DayNumber - contractStartDate.DayNumber;
        if (daysDifference is < 3 or > 30)
            throw new DomainException("Contract time period must be between 3 and 30 days");
    }
}