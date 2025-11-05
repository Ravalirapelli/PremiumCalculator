using PremiumCalculator.API.Models;

namespace PremiumCalculator.API.Services;

public interface IPremiumCalculatorService
{
    PremiumCalculationResponse CalculatePremium(PremiumCalculationRequest request);
}

