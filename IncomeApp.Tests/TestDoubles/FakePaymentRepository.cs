using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakePaymentRepository : IPaymentRepository
{
    private readonly List<Payment> _payments =
    [
        new Payment
        {
            IdPayment = 1,
            IdContract = 1,
            IdSubscription = null,
            IdCustomer = 1,
            Amount = 1000,
            IdContractNav = new Contract
            {
                IdContract = 1,
                StartDate = new DateOnly(2022, 1, 1),
                EndDate = new DateOnly(2022, 1, 15),
                Cost = 1000,
                YearsOfUpdates = 1,
                SoftwareVersion = "1.0",
                IdCustomer = 3,
                IdSoftware = 1,
                IsSigned = true
            }
        },

        new Payment
        {
            IdPayment = 2,
            IdContract = null,
            IdSubscription = 1,
            IdCustomer = 2,
            Amount = 2000,
            IdSubscriptionNav = new Subscription
            {
                IdSubscription = 1,
                Name = "Subscription1",
                DurationInMonths = 12,
                LastRenewal = new DateOnly(2022, 1, 1),
                Price = 1000,
                IdCustomer = 2,
                IdSoftware = 2
            }
        }
    ];

    public Task<int> AddPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        _payments.Add(payment);
        return Task.FromResult(payment.IdPayment);
    }

    public Task<IEnumerable<Payment>> GetPaymentsWithSubscriptionsAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_payments.AsEnumerable());
    }
}