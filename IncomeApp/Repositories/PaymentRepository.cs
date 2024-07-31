using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class PaymentRepository(Context context) : IPaymentRepository
{
    public async Task<int> AddPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        await context.Payments.AddAsync(payment, cancellationToken);
        return payment.IdPayment;
    }

    public async Task<IEnumerable<Payment>> GetPaymentsWithSubscriptionsAsync(CancellationToken cancellationToken)
    {
        return await context.Payments
            .Where(p => p.IdContract == null)
            .Include(p => p.IdSubscriptionNav)
            .ToListAsync(cancellationToken);
    }
}