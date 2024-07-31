using IncomeApp.DTOs.Request;
using IncomeApp.Exceptions;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class SubscriptionService(
    ISubscriptionRepository subscriptionRepository,
    ISoftwareRepository softwareRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : ISubscriptionService
{
    public async Task<int> CreateSubscriptionAsync(SubscriptionCreateDTO subscription,
        CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);

        var software = await GetRelatedSoftwareAsync(subscription.IdSoftware, cancellationToken);
        var customer = await GetRelatedCustomerAsync(subscription.IdCustomer, cancellationToken);

        var softwareDiscount = FindBestPossibleActiveDiscount(software.Discounts);
        var customerDiscount = CheckIfEligibleForCustomerDiscount(customer);
        var totalDiscount = softwareDiscount + customerDiscount;

        var yearlyCost = software.YearlyCost;
        var costPerRenewal = yearlyCost / 12 * subscription.DurationInMonths * (1 - (decimal)totalDiscount / 100);

        var id = await subscriptionRepository.CreateSubscriptionAsync(new Subscription
        {
            Name = software.Name,
            DurationInMonths = subscription.DurationInMonths,
            LastRenewal = DateOnly.FromDateTime(DateTime.Now),
            Price = costPerRenewal,
            IdCustomer = subscription.IdCustomer,
            IdSoftware = subscription.IdSoftware
        }, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return id;
    }

    private static int FindBestPossibleActiveDiscount(ICollection<Discount> softwareDiscounts)
    {
        return softwareDiscounts.Where(d => d.StartDate <= DateOnly.FromDateTime(DateTime.Now) &&
                                            d.EndDate >= DateOnly.FromDateTime(DateTime.Now))
            .MaxBy(d => d.Percentage)
            ?.Percentage ?? 0;
    }

    private static int CheckIfEligibleForCustomerDiscount(Customer customer)
    {
        return customer.Contracts.Any(c => c.IsSigned) ? 5 : 0;
    }

    private async Task<Customer> GetRelatedCustomerAsync(int subscriptionIdCustomer,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetCustomerAsync(subscriptionIdCustomer, cancellationToken);
        if (customer == null) throw new DomainException("Customer not found");
        return customer;
    }

    private async Task<Software> GetRelatedSoftwareAsync(int subscriptionIdSoftware,
        CancellationToken cancellationToken)
    {
        var software = await softwareRepository.GetSoftwareAsync(subscriptionIdSoftware, cancellationToken);
        if (software == null) throw new DomainException("Software not found");
        return software;
    }
}