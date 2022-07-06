using CSharpPlayground.services;

using System.Web.Http;

namespace ApiPlayground.controllers;


public class CalculatorController : ApiController
{
    private readonly ILeftToRightCalculator _leftToRightCalculator;
    private readonly IBidmasCalculator _bidmascalculator;
    
    public CalculatorController(IBidmasCalculator bidmasCalculator, ILeftToRightCalculator leftToRightCalculator)
    {
        _leftToRightCalculator = leftToRightCalculator;
        _bidmascalculator = bidmasCalculator;
    }
    
    [Microsoft.AspNetCore.Mvc.Route("calculate")]
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public string CalculateBidmas(string calculation)
    {
        return _bidmascalculator.Calculate(calculation);
    }
    
    [Microsoft.AspNetCore.Mvc.Route("calculate/ltr")]
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public string CalculateLtr(string calculation, string ltr)
    {

        return _leftToRightCalculator.Calculate(calculation);
    }
}