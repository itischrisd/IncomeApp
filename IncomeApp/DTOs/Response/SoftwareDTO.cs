namespace IncomeApp.DTOs.Response;

public class SoftwareDTO
{
    public int IdSoftware { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CurrentVersion { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal YearlyCost { get; set; }
    public ICollection<DiscountDTO> Discounts { get; set; } = new List<DiscountDTO>();
}