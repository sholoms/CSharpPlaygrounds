
using ApiPlayground.Configuration;
using ApiPlayground.services.interfaces;
using Microsoft.Extensions.Options;

namespace ApiPlayground.controllers;


public class CalculatorControllerImplementation : ICalculatorController
{
    private readonly ILeftToRightCalculator _leftToRightCalculator;
    private readonly IOptions<FileSettings> _fileSettings;
    private readonly IFileService _fileServie;
    private readonly IBidmasCalculator _bidmasCalculator;
    
    public CalculatorControllerImplementation(IBidmasCalculator bidmasCalculator, ILeftToRightCalculator leftToRightCalculator, IOptions<FileSettings> fileSettings, IFileService fileServie)
    {
        _leftToRightCalculator = leftToRightCalculator;
        _fileSettings = fileSettings;
        _fileServie = fileServie;
        _bidmasCalculator = bidmasCalculator;
    }
    public Task<CalculationResponse> CalculatePostAsync(CalculationRequest body)
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

    public Task<CalculationResponse> CalculateGetAsync(string calculation)
    {
        var result = _bidmasCalculator.Calculate(calculation);
        var response = new CalculationResponse
        {
            Result = result
        };
        return Task.FromResult(response);
    }

    public async Task<FileResultResponse> CalculateFileAsync()
    {
        return await _fileServie.ReadFile();
    }

    public async Task<FileResultResponse> AddToFileAsync(AddToFileRequest body)
    {
        await _fileServie.WriteFile(body);
        return await _fileServie.ReadFile();
    }
}