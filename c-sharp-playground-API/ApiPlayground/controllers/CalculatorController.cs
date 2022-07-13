using System.Web.Http;
using ApiPlayground.Models;
using ApiPlayground.services;

namespace ApiPlayground.controllers;


public class CalculatorController : ApiController
{
    private readonly ILeftToRightCalculator _leftToRightCalculator;
    private readonly IBidmasCalculator _bidmasCalculator;
    
    public CalculatorController(IBidmasCalculator bidmasCalculator, ILeftToRightCalculator leftToRightCalculator)
    {
        _leftToRightCalculator = leftToRightCalculator;
        _bidmasCalculator = bidmasCalculator;
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("calculate")]
    public Response GetCalculateBidmas(string calculation)
    {
        var result = _bidmasCalculator.Calculate(calculation);
        var response = new Response
        {
            Result = result
        };
        
        return response;
    }
    
    [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.Route("calculate")]
    public Task<Response> PostCalculateBidmas([Microsoft.AspNetCore.Mvc.FromBody] CalculationRequest body)
    {
        string result;
        if (body.Ltr)
        {
            result = _leftToRightCalculator.Calculate(body.Calculation);
        }
        else
        {
            result = _bidmasCalculator.Calculate(body.Calculation);
        }
        
        var response = new Response
        {
            Result = result
        };
        
        return Task.FromResult(response);
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("calculate/ltr")]
    public Response CalculateLtr(string calculation, string ltr)
    {

        var result = _leftToRightCalculator.Calculate(calculation);
        return new Response
        {
            Result = result
        };
    }
}