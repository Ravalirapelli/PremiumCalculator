namespace PremiumCalculator.API.Models;

public class PremiumCalculationResponse
{
    public decimal MonthlyPremium { get; set; }
    public string OccupationRating { get; set; } = string.Empty;
    public decimal OccupationRatingFactor { get; set; }
}

