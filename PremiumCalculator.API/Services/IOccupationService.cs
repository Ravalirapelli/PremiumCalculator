using PremiumCalculator.API.Models;

namespace PremiumCalculator.API.Services;

public interface IOccupationService
{
    List<Occupation> GetOccupations();
    Occupation? GetOccupationByName(string name);
}

