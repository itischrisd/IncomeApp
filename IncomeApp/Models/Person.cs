namespace IncomeApp.Models;

public class Person : Customer
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public bool IsDeleted { get; set; }
}