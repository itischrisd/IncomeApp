namespace IncomeApp.DTOs.Response;

public class PersonDTO
{
    public int IdPerson { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Pesel { get; set; } = null!;
}