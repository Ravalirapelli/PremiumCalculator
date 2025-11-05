using System.ComponentModel.DataAnnotations;

namespace PremiumCalculator.API.Models;

public class PremiumCalculationRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
    public int AgeNextBirthday { get; set; }

    [Required]
    public string DateOfBirth { get; set; } = string.Empty; // mm/YYYY format

    [Required]
    public string UsualOccupation { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Death Sum Insured must be greater than 0")]
    public decimal DeathSumInsured { get; set; }
}

