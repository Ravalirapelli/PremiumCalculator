using PremiumCalculator.API.Models;

namespace PremiumCalculator.API.Services;

public class OccupationService : IOccupationService
{
    private readonly List<Occupation> _occupations = new()
    {
        new Occupation { Name = "Cleaner", Rating = "Light Manual", RatingFactor = 11.50m },
        new Occupation { Name = "Doctor", Rating = "Professional", RatingFactor = 1.5m },
        new Occupation { Name = "Author", Rating = "White Collar", RatingFactor = 2.25m },
        new Occupation { Name = "Farmer", Rating = "Heavy Manual", RatingFactor = 31.75m },
        new Occupation { Name = "Mechanic", Rating = "Heavy Manual", RatingFactor = 31.75m },
        new Occupation { Name = "Florist", Rating = "Light Manual", RatingFactor = 11.50m },
        new Occupation { Name = "Other", Rating = "Heavy Manual", RatingFactor = 31.75m }
    };

    public List<Occupation> GetOccupations()
    {
        return _occupations;
    }

    public Occupation? GetOccupationByName(string name)
    {
        return _occupations.FirstOrDefault(o => o.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}

