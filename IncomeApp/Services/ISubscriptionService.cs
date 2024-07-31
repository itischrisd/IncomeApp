using IncomeApp.DTOs.Request;

namespace IncomeApp.Services;

public interface ISubscriptionService
{
    Task<int> CreateSubscriptionAsync(SubscriptionCreateDTO subscription, CancellationToken cancellationToken);
}