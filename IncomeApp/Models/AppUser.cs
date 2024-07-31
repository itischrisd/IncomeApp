namespace IncomeApp.Models;

public class AppUser
{
    public int IdUser { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime? RefreshTokenExp { get; set; }
    public List<string> Roles { get; set; } = [];
}