using PremiumCalculator.API.Models;

namespace PremiumCalculator.API.Services;

public class PremiumCalculatorService : IPremiumCalculatorService
{
    private readonly IOccupationService _occupationService;

    public PremiumCalculatorService(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    public PremiumCalculationResponse CalculatePremium(PremiumCalculationRequest request)
    {
        var occupation = _occupationService.GetOccupationByName(request.UsualOccupation);
        
        if (occupation == null)
        {
            throw new ArgumentException($"Occupation '{request.UsualOccupation}' not found.");
        }

        // Formula: (Death Cover amount * Occupation Rating Factor * Age) / 1000 * 12
        var monthlyPremium = (request.DeathSumInsured * occupation.RatingFactor * request.AgeNextBirthday) / 1000m * 12m;

        return new PremiumCalculationResponse
        {
            MonthlyPremium = monthlyPremium,
            OccupationRating = occupation.Rating,
            OccupationRatingFactor = occupation.RatingFactor
        };
    }
}

