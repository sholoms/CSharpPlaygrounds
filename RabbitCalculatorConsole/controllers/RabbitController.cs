using Emn.DataAccess;
using RabbitCalculatorConsole.Services;

namespace RabbitCalculatorConsole.controllers;

public class RabbitController : IProgramController
{
    private readonly IRabbitCalculator _apiCalculator;

    public RabbitController(IRabbitCalculator apiCalculator)
    {
        _apiCalculator = apiCalculator;
    }
    public async Task Run()
    {
        Console.WriteLine("Please write the sum to calculate");
        Console.WriteLine("type 'q' to quit");
        var input = Console.ReadLine();
        // Calculator calculator = new ();
        while (input != "q")
        {
            Console.WriteLine("---------------------");
            // var result = _calculator.Calculate(input);
            var result = input?.Decrypt();
            // var result = await _apiCalculator.Calculate(input);
            Console.WriteLine(result);
            // Console.WriteLine("---------------------");
            // Console.WriteLine("Please write the sum to calculate");
            // Console.WriteLine("type 'q' to quit");
            
            input = Console.ReadLine();
        }

        //_apiCalculator.close();
    }
}