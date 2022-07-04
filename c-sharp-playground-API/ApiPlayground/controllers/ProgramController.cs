using CSharpPlayground.services;

using System.Web.Http;

namespace ApiPlayground.controllers;

[Microsoft.AspNetCore.Mvc.Route("calculate/{calculatorType}/{calculation}")]
public class ProgramController : ApiController
{
    private readonly ILeftToRightCalculator _leftToRightCalculator;
    private readonly IBidmasCalculator _bidmascalculator;
    
    public ProgramController(IBidmasCalculator bidmasCalculator, ILeftToRightCalculator leftToRightCalculator)
    {
        _leftToRightCalculator = leftToRightCalculator;
        _bidmascalculator = bidmasCalculator;
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public string Run(string calculation, string calculatorType)
    {
        
        return calculatorType == "ltr" ? _leftToRightCalculator.Calculate(calculation) : _bidmascalculator.Calculate(calculation);
    }
}