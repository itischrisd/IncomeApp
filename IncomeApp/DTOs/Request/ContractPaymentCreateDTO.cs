using System.ComponentModel.DataAnnotations;

namespace IncomeApp.DTOs.Request;

public class ContractPaymentCreateDTO
{
    [Required]
    public int IdContract { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }
}