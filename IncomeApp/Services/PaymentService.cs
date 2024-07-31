using IncomeApp.DTOs.Request;
using IncomeApp.Exceptions;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class PaymentService(
    IPaymentRepository paymentRepository,
    IContractRepository contractRepository,
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork) : IPaymentService
{
    public async Task<int> AddContractPaymentAsync(ContractPaymentCreateDTO payment,
        CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);

        var contract = await GetRelatedContractWithAssociatedPayments(payment, cancellationToken);

        EnsureContractIsActive(contract);
        EnsureContractIsNotSigned(contract);
        EnsurePaymentAmountIsValid(payment.Amount, contract);

        var id = await paymentRepository.AddPaymentAsync(new Payment
        {
            IdContract = payment.IdContract,
            IdCustomer = contract.IdCustomer,
            Amount = payment.Amount
        }, cancellationToken);

        MarkContractAsSignedIfPayedFully(contract);

        await unitOfWork.CommitAsync(cancellationToken);

        return id;
    }

    public async Task<int> AddSubscriptionPaymentAsync(SubscriptionPaymentCreateDTO payment,
        CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);

        var subscription = await GetRelatedSubscription(payment, cancellationToken);

        EnsureSubscriptionIsActive(subscription);
        EnsureSubscriptionIsNotPaidFor(subscription);
        EnsurePaymentIsEqualToSubscriptionCost(payment.Amount, subscription);

        var id = await paymentRepository.AddPaymentAsync(new Payment
        {
            IdSubscription = payment.IdSubscription,
            IdCustomer = subscription.IdCustomer,
            Amount = payment.Amount
        }, cancellationToken);

        subscription.LastRenewal = subscription.LastRenewal.AddMonths(subscription.DurationInMonths);

        await unitOfWork.CommitAsync(cancellationToken);

        return id;
    }

    private static void EnsurePaymentIsEqualToSubscriptionCost(decimal paymentAmount, Subscription subscription)
    {
        if (paymentAmount != subscription.Price)
            throw new DomainException("Payment amount is not equal to subscription cost");
    }

    private static void EnsureSubscriptionIsNotPaidFor(Subscription subscription)
    {
        if (subscription.LastRenewal > DateOnly.FromDateTime(DateTime.Now))
            throw new DomainException("Subscription is already paid for");
    }

    private static void EnsureSubscriptionIsActive(Subscription subscription)
    {
        if (subscription.LastRenewal.AddMonths(subscription.DurationInMonths) < DateOnly.FromDateTime(DateTime.Now))
            throw new DomainException("Subscription is not active");
    }

    private async Task<Subscription> GetRelatedSubscription(SubscriptionPaymentCreateDTO payment,
        CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.GetSubscriptionAsync(payment.IdSubscription, cancellationToken);
        if (subscription == null)
            throw new DomainException("Subscription not found");
        return subscription;
    }

    private async Task<Contract> GetRelatedContractWithAssociatedPayments(ContractPaymentCreateDTO payment,
        CancellationToken cancellationToken)
    {
        var contract = await contractRepository.GetContractAsync(payment.IdContract, cancellationToken);
        if (contract == null)
            throw new DomainException("Contract not found");
        return contract;
    }

    private static void MarkContractAsSignedIfPayedFully(Contract contract)
    {
        if (contract.Payments.Sum(p => p.Amount) == contract.Cost)
            contract.IsSigned = true;
    }

    private static void EnsureContractIsNotSigned(Contract contract)
    {
        if (contract.IsSigned)
            throw new DomainException("Contract is already signed");
    }

    private static void EnsurePaymentAmountIsValid(decimal paymentAmount, Contract contract)
    {
        var totalPayments = contract.Payments.Sum(p => p.Amount);
        if (totalPayments + paymentAmount > contract.Cost)
            throw new DomainException("Payment amount exceeds contract amount");
    }

    private static void EnsureContractIsActive(Contract contract)
    {
        if (DateOnly.FromDateTime(DateTime.Now) < contract.StartDate ||
            DateOnly.FromDateTime(DateTime.Now) > contract.EndDate)
            throw new DomainException("Contract is not active");
    }
}