using System.ComponentModel.DataAnnotations;

namespace IncomeApp.DTOs.Request;

public class CompanyCreateDTO
{
    [Required]
    [Length(2, 100)]
    public string Name { get; set; } = null!;

    [Required]
    [Length(2, 100)]
    public string Address { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Length(2, 100)]
    public string Email { get; set; } = null!;

    [Required]
    [Length(9, 9)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [Length(9, 14)]
    public string KRS { get; set; } = null!;
}