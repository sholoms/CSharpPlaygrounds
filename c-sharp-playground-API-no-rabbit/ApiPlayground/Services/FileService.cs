using ApiPlayground.Configuration;
using ApiPlayground.controllers;
using ApiPlayground.services.interfaces;
using Microsoft.Extensions.Options;

namespace ApiPlayground.services;

public class FileService : IFileService
{
    private readonly IOptions<FileSettings> _fileSettings;
    private readonly IBidmasCalculator _bidmasCalculator;

    public FileService(IOptions<FileSettings> fileSettings, IBidmasCalculator bidmasCalculator)
    {
        _fileSettings = fileSettings;
        _bidmasCalculator = bidmasCalculator;
    }
    
    public async Task<FileResultResponse> ReadFile()
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
            Results = results
        };
    }
    
    public async Task WriteFile(AddToFileRequest lines)
    
    {
        await using var writer = new StreamWriter(_fileSettings.Value.FilePath, append: true);
        foreach (var line in lines.Calculations)
        {
            await writer.WriteLineAsync(line);
        }
    }
    
    public async Task WriteFile(string line)
    {
        await using var writer = new StreamWriter(_fileSettings.Value.FilePath, append: true);
        await writer.WriteLineAsync(line);
    }
}