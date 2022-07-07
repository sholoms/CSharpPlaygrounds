using CSharpPlayground.services;

using System.Web.Http;
using ApiPlayground.Models;

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
    public Response CalculateBidmas(string calculation)
    {
        var result = _bidmascalculator.Calculate(calculation);
        return new Response
        {
            Result = result
        };
    }
    
    [Microsoft.AspNetCore.Mvc.Route("calculate/ltr")]
    [Microsoft.AspNetCore.Mvc.HttpGet]
    public Response CalculateLtr(string calculation, string ltr)
    {

        var result = _leftToRightCalculator.Calculate(calculation);
        return new Response
        {
            Result = result
        };
    }
}