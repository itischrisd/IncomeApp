namespace IncomeApp.DTOs.Response;

public class CompanyDTO
{
    public int IdCompany { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string KRS { get; set; } = null!;
}