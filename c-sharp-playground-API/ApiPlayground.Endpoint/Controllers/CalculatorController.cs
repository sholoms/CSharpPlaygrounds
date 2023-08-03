
using ApiPlayground.Configuration;
using ApiPlayground.services.interfaces;
using DbPlayground.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApiPlayground.controllers;


public class CalculatorControllerImplementation : ICalculatorController
{
    private readonly ILeftToRightCalculator _leftToRightCalculator;
    private readonly IOptions<FileSettings> _fileSettings;
    private readonly IFileService _fileService;
    private readonly CalculatorContext _dbContext;
    private readonly IBidmasCalculator _bidmasCalculator;
    
    public CalculatorControllerImplementation(IBidmasCalculator bidmasCalculator, ILeftToRightCalculator leftToRightCalculator, IOptions<FileSettings> fileSettings, IFileService fileService, CalculatorContext dbContext)
    {
        _leftToRightCalculator = leftToRightCalculator;
        _fileSettings = fileSettings;
        _fileService = fileService;
        _dbContext = dbContext;
        _bidmasCalculator = bidmasCalculator;
        _dbContext = dbContext;
    }
    public async Task<CalculationResponse> CalculatePostAsync(CalculationRequest body)
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
        var db = await _dbContext.Equations.ToListAsync();
        Console.WriteLine(db.First().Calculation);
        return response;
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

    public async Task<StorageResultResponse> CalculateFileAsync()
    {
        return await _fileService.ReadFile();
    }

    public Task<StorageResultResponse> AddToDbAsync(AddToStorageRequest body)
    {
        throw new NotImplementedException();
    }

    public Task<StorageResultResponse> CalculateDbAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<StorageResultResponse> AddToFileAsync(AddToStorageRequest body)
    {
        await _fileService.WriteFile(body);
        return await _fileService.ReadFile();
    }
}