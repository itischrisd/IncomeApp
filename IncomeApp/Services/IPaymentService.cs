using IncomeApp.DTOs.Request;

namespace IncomeApp.Services;

public interface IPaymentService
{
    Task<int> AddContractPaymentAsync(ContractPaymentCreateDTO payment, CancellationToken cancellationToken);
    Task<int> AddSubscriptionPaymentAsync(SubscriptionPaymentCreateDTO payment, CancellationToken cancellationToken);
}