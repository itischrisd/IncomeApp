namespace IncomeApp.Models;

public abstract class Customer
{
    public int IdCustomer { get; set; }
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}