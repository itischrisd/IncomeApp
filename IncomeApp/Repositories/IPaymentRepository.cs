using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface IPaymentRepository
{
    Task<int> AddPaymentAsync(Payment payment, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsWithSubscriptionsAsync(CancellationToken cancellationToken);
}