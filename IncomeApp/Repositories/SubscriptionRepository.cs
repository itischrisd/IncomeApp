using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class SubscriptionRepository(Context context) : ISubscriptionRepository
{
    public async Task<int> CreateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        return (await context.Subscriptions.AddAsync(subscription, cancellationToken)).Entity.IdSubscription;
    }

    public async Task<Subscription> GetSubscriptionAsync(int paymentIdSubscription, CancellationToken cancellationToken)
    {
        return (await context.Subscriptions
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(s => s.IdSubscription == paymentIdSubscription, cancellationToken))!;
    }
}