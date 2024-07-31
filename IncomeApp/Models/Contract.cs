namespace IncomeApp.Models;

public class Contract
{
    public int IdContract { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Cost { get; set; }
    public int YearsOfUpdates { get; set; }
    public string SoftwareVersion { get; set; } = null!;
    public bool IsSigned { get; set; }
    public int IdCustomer { get; set; }
    public int IdSoftware { get; set; }

    public virtual Customer IdCustomerNav { get; set; } = null!;
    public virtual Software IdSoftwareNav { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}