using System.Web.Http;
using ApiPlayground.Configuration;
using ApiPlayground.Models;
using ApiPlayground.services;
using Microsoft.Extensions.Options;

namespace ApiPlayground.controllers;


public class CalculatorController : ApiController
{
    private readonly ILeftToRightCalculator _leftToRightCalculator;
    private readonly IOptions<FileSettings> _fileSettings;
    private readonly IBidmasCalculator _bidmasCalculator;
    
    public CalculatorController(IBidmasCalculator bidmasCalculator, ILeftToRightCalculator leftToRightCalculator, IOptions<FileSettings> fileSettings)
    {
        _leftToRightCalculator = leftToRightCalculator;
        _fileSettings = fileSettings;
        _bidmasCalculator = bidmasCalculator;
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("calculate")]
    public CalculationResponse GetCalculateBidmas(string calculation)
    {
        var result = _bidmasCalculator.Calculate(calculation);
        var response = new CalculationResponse
        {
            Result = result
        };
        
        return response;
    }
    
    [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.Route("calculate")]
    public Task<CalculationResponse> PostCalculateBidmas([Microsoft.AspNetCore.Mvc.FromBody] CalculationRequest body)
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
        
        var response = new CalculationResponse
        {
            Result = result
        };
        
        return Task.FromResult(response);
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("calculate/ltr")]
    public CalculationResponse CalculateLtr(string calculation, string ltr)
    {

        var result = _leftToRightCalculator.Calculate(calculation);
        return new CalculationResponse
        {
            Result = result
        };
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("file/results")]
    public async Task<FileResultResponse> CalculateFile(string calculation, string ltr)
    {
        using var reader = new StreamReader(_fileSettings.Value.FilePath);
        List<CalculationResponse> results = new List<CalculationResponse>();
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            var result = new CalculationResponse()
            {
                Result = _bidmasCalculator.Calculate(line)
            };

            results.Add(result);
        }
        return new FileResultResponse()
        {
            Results = results.ToArray()
        };
    }
    
    [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("docker")]
    public CalculationResponse CalculateDockerTest(string calculation, string ltr)
    {
        return new CalculationResponse()
        {
            Result = _bidmasCalculator.Calculate(_fileSettings.Value.DockerTest)
        };
    }
}