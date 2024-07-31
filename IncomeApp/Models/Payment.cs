namespace IncomeApp.Models;

public class Payment
{
    public int IdPayment { get; set; }
    public int? IdContract { get; set; }
    public int? IdSubscription { get; set; }
    public int IdCustomer { get; set; }
    public decimal Amount { get; set; }

    public virtual Contract IdContractNav { get; set; } = null!;
    public virtual Customer IdCustomerNav { get; set; } = null!;
    public virtual Subscription IdSubscriptionNav { get; set; } = null!;
}