using Microsoft.AspNetCore.Mvc;
using PremiumCalculator.API.Models;
using PremiumCalculator.API.Services;

namespace PremiumCalculator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PremiumController : ControllerBasex1S
{
    private readonly IPremiumCalculatorService _premiumCalculatorService;
    private readonly IOccupationService _occupationService;

    public PremiumController(
        IPremiumCalculatorService premiumCalculatorService,
        IOccupationService occupationService)
    {
        _premiumCalculatorService = premiumCalculatorService;
        _occupationService = occupationService;
    }

    [HttpGet("occupations")]
    public ActionResult<List<Occupation>> GetOccupations()
    {
        var occupations = _occupationService.GetOccupations();
        return Ok(occupations);
    }

    [HttpPost("calculate")]
    public ActionResult<PremiumCalculationResponse> CalculatePremium([FromBody] PremiumCalculationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = _premiumCalculatorService.CalculatePremium(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

