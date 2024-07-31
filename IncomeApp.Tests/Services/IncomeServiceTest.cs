using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Services;
using IncomeApp.Tests.TestDoubles;
using JetBrains.Annotations;
using Xunit;

namespace IncomeApp.Tests.Services;

[TestSubject(typeof(IncomeService))]
public class IncomeServiceTest
{
    [Fact]
    public async Task GetIncome_Should_Return_Income()
    {
        var paymentRepository = new FakePaymentRepository();
        var contractRepository = new FakeContractRepository();
        var incomeService = new IncomeService(paymentRepository, contractRepository);

        var result = await incomeService.GetIncomeAsync(CancellationToken.None);

        Assert.Equal(2000, result);
    }

    [Fact]
    public async Task GetIncomeWithCurrency_Should_Return_Income_With_Currency()
    {
        var paymentRepository = new FakePaymentRepository();
        var contractRepository = new FakeContractRepository();
        var incomeService = new IncomeService(paymentRepository, contractRepository);

        var result = await incomeService.GetIncomeWithCurrencyAsync("USD", CancellationToken.None);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task GetExpectedIncomes_Should_Return_Expected_Incomes()
    {
        var paymentRepository = new FakePaymentRepository();
        var contractRepository = new FakeContractRepository();
        var incomeService = new IncomeService(paymentRepository, contractRepository);

        var result = await incomeService.GetExpectedIncomesAsync(CancellationToken.None);

        Assert.Equal(23000, result);
    }

    [Fact]
    public async Task GetExpectedIncome_Should_Return_Expected_Income_For_Product()
    {
        var paymentRepository = new FakePaymentRepository();
        var contractRepository = new FakeContractRepository();
        var incomeService = new IncomeService(paymentRepository, contractRepository);

        var result = await incomeService.GetExpectedIncomeAsync(1, CancellationToken.None);

        Assert.Equal(5000, result);
    }

    [Fact]
    public async Task GetIncome_Should_Return_Income_For_Product()
    {
        var paymentRepository = new FakePaymentRepository();
        var contractRepository = new FakeContractRepository();
        var incomeService = new IncomeService(paymentRepository, contractRepository);

        var result = await incomeService.GetIncomeAsync(2, CancellationToken.None);

        Assert.Equal(2000, result);
    }
}