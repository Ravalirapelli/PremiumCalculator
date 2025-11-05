namespace PremiumCalculator.API.Models;

public class Occupation
{
    public string Name { get; set; } = string.Empty;
    public string Rating { get; set; } = string.Empty;
    public decimal RatingFactor { get; set; }
}

