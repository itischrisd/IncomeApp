using System.ComponentModel.DataAnnotations;

namespace IncomeApp.DTOs.Request;

public class SubscriptionPaymentCreateDTO
{
    [Required]
    public int IdSubscription { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }
}