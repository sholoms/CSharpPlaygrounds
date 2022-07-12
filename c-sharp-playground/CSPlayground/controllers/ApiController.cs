using System;
using System.IO;
using System.Threading.Tasks;
using CSPlayground.services;

namespace CSPlayground.controllers;

public class ApiController : IProgramController
{
    private readonly IApiCalculator _apiCalculator;

    public ApiController(IApiCalculator apiCalculator)
    {
        _apiCalculator = apiCalculator;
    }
    public async Task Run(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            using var reader = new StreamReader("./../../../input.txt");
            Console.WriteLine("Start file results");
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var result = await _apiCalculator.Calculate(line);
                
                Console.WriteLine(result);
            }
            Console.WriteLine("End file results");
        }
        Console.WriteLine("Please write the sum to calculate");
        Console.WriteLine("type 'q' to quit");
        var input = Console.ReadLine();
        // Calculator calculator = new ();
        while (input != "q")
        {
            Console.WriteLine("---------------------");
            //var result = _calculator.Calculate(input);
            string result = await _apiCalculator.Calculate(input);
            Console.WriteLine(result);
            Console.WriteLine("---------------------");
            Console.WriteLine("Please write the sum to calculate");
            Console.WriteLine("type 'q' to quit");
            input = Console.ReadLine();
        }
    }
}