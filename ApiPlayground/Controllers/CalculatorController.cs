using ApiPlayground.Models;
using ApiPlayground.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiPlayground.Controllers;

[ApiController]
[Route("calculation-result")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculatorService _calculatorService;

    public CalculatorController(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
    }
    [HttpGet("{query}")]
    public CalculationResult Get(string query)
    {
        return new CalculationResult
        {
            result = _calculatorService.Calculate(query)
        };
    }
}
 