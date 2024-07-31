namespace IncomeApp.Models;

public class Subscription
{
    public int IdSubscription { get; set; }
    public string Name { get; set; } = null!;
    public int DurationInMonths { get; set; }
    public DateOnly LastRenewal { get; set; }
    public decimal Price { get; set; }
    public int IdCustomer { get; set; }
    public int IdSoftware { get; set; }

    public virtual Customer Customer { get; set; } = null!;
    public virtual Software Software { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}