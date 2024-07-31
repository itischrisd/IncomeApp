namespace IncomeApp.Models;

public class Discount
{
    public int IdDiscount { get; set; }
    public string Name { get; set; } = null!;
    public string Offer { get; set; } = null!;
    public int Percentage { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int IdSoftware { get; set; }

    public virtual Software IdSoftwareNav { get; set; } = null!;
}