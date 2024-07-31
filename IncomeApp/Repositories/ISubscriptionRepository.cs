using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface ISubscriptionRepository
{
    Task<int> CreateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken);
    Task<Subscription> GetSubscriptionAsync(int paymentIdSubscription, CancellationToken cancellationToken);
}