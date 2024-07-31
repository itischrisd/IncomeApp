using System.ComponentModel.DataAnnotations;

namespace IncomeApp.DTOs.Request;

public class PersonCreateDTO
{
    [Required]
    [Length(2, 100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [Length(2, 100)]
    public string LastName { get; set; } = null!;

    [Required]
    [Length(2, 100)]
    public string Address { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Length(2, 100)]
    public string Email { get; set; } = null!;

    [Required]
    [Length(9, 9)]
    [RegularExpression(@"^\d{9}$")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [Length(11, 11)]
    [RegularExpression(@"^\d{11}$")]
    public string Pesel { get; set; } = null!;
}