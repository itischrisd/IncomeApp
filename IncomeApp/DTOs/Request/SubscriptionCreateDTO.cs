using System.ComponentModel.DataAnnotations;

namespace IncomeApp.DTOs.Request;

public class SubscriptionCreateDTO
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(1, 24)]
    public int DurationInMonths { get; set; }

    [Required]
    public int IdCustomer { get; set; }

    [Required]
    public int IdSoftware { get; set; }
}