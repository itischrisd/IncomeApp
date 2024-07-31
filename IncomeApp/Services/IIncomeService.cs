namespace IncomeApp.Services;

public interface IIncomeService
{
    Task<decimal> GetIncomeAsync(CancellationToken cancellationToken);
    Task<decimal> GetIncomeAsync(int productId, CancellationToken cancellationToken);
    Task<decimal> GetIncomeWithCurrencyAsync(string currency, CancellationToken cancellationToken);
    Task<decimal> GetExpectedIncomesAsync(CancellationToken cancellationToken);
    Task<decimal> GetExpectedIncomeAsync(int productId, CancellationToken cancellationToken);
    Task<decimal> GetExpectedIncomeWithCurrencyAsync(string currency, CancellationToken cancellationToken);
}