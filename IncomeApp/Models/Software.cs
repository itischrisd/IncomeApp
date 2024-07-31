namespace IncomeApp.Models;

public class Software
{
    public int IdSoftware { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CurrentVersion { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal YearlyCost { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}