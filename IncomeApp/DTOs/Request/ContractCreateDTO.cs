using System.ComponentModel.DataAnnotations;

namespace IncomeApp.DTOs.Request;

public class ContractCreateDTO
{
    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    [Required]
    [Range(1, 4)]
    public int YearsOfUpdates { get; set; }

    [Required]
    public int IdCustomer { get; set; }

    [Required]
    public int IdSoftware { get; set; }
}