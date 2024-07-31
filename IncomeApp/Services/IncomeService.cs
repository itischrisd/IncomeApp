using System.Text.Json;
using IncomeApp.Exceptions;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class IncomeService(IPaymentRepository paymentRepository, IContractRepository contractRepository)
    : IIncomeService
{
    public async Task<decimal> GetIncomeAsync(CancellationToken cancellationToken)
    {
        var paymentsFromSubscriptions = await GetPaymentsOfSybscriptionsAsync(cancellationToken);
        var contracts = await GetContractsAsync(cancellationToken);
        var signedContracts = contracts.ToList()
            .Where(c => c.IsSigned);

        var subscriptionIncome = paymentsFromSubscriptions.Sum(p => p.Amount);
        var contractIncome = signedContracts.Sum(c => c.Cost);

        return subscriptionIncome + contractIncome;
    }

    public async Task<decimal> GetIncomeAsync(int productId, CancellationToken cancellationToken)
    {
        var paymentsFromSubscriptions = await GetPaymentsOfSybscriptionsAsync(cancellationToken);
        var contracts = await GetContractsAsync(cancellationToken);
        var signedContracts = contracts.ToList()
            .Where(c => c.IsSigned);

        var subscriptionIncome = paymentsFromSubscriptions.Where(p => p.IdSubscriptionNav.IdSoftware == productId)
            .Sum(p => p.Amount);
        var contractIncome = signedContracts.Where(c => c.IdSoftware == productId)
            .Sum(c => c.Cost);

        return subscriptionIncome + contractIncome;
    }

    public async Task<decimal> GetIncomeWithCurrencyAsync(string currency, CancellationToken cancellationToken)
    {
        var rate = await GetRateAsync(currency, cancellationToken);
        var income = await GetIncomeAsync(cancellationToken);

        return income * (decimal)rate;
    }

    public async Task<decimal> GetExpectedIncomesAsync(CancellationToken cancellationToken)
    {
        var paymentsFromSubscriptions = await GetPaymentsOfSybscriptionsAsync(cancellationToken);
        var contracts = await GetContractsAsync(cancellationToken);

        var subscriptionIncome = paymentsFromSubscriptions.Sum(p => p.Amount);
        var contractIncome = contracts.Sum(c => c.Cost);

        return subscriptionIncome + contractIncome;
    }

    public async Task<decimal> GetExpectedIncomeAsync(int productId, CancellationToken cancellationToken)
    {
        var paymentsFromSubscriptions = await GetPaymentsOfSybscriptionsAsync(cancellationToken);
        var contracts = await GetContractsAsync(cancellationToken);

        var subscriptionIncome = paymentsFromSubscriptions.Where(p => p.IdSubscriptionNav.IdSoftware == productId)
            .Sum(p => p.Amount);
        var contractIncome = contracts.Where(c => c.IdSoftware == productId)
            .Sum(c => c.Cost);

        return subscriptionIncome + contractIncome;
    }

    public async Task<decimal> GetExpectedIncomeWithCurrencyAsync(string currency, CancellationToken cancellationToken)
    {
        var rate = await GetRateAsync(currency, cancellationToken);
        var income = await GetExpectedIncomesAsync(cancellationToken);

        return income * (decimal)rate;
    }

    private async Task<List<Contract>> GetContractsAsync(CancellationToken cancellationToken)
    {
        return (await contractRepository.GetContractsAsync(cancellationToken)).ToList();
    }

    private async Task<IEnumerable<Payment>> GetPaymentsOfSybscriptionsAsync(CancellationToken cancellationToken)
    {
        var payments = await paymentRepository.GetPaymentsWithSubscriptionsAsync(cancellationToken);
        return payments.Where(p => p.IdContract == null);
    }

    private static async Task<object> GetRateAsync(string currency, CancellationToken cancellationToken)
    {
        const string apiUrl = "https://api.exchangerate-api.com/v4/latest/PLN";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode) throw new DomainException("Failed to get exchange rate");

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        using var document = JsonDocument.Parse(content);
        var root = document.RootElement;
        var rates = root.GetProperty("rates");

        if (!rates.TryGetProperty(currency, out var rate)) throw new DomainException("Currency not found");

        return rate.GetDecimal();
    }
}