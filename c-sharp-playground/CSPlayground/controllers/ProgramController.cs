using CSharpPlayground.services;

namespace CSharpPlayground.controllers;

public class ProgramController : IProgramController
{
    private readonly ICalculator _calculator;

    public ProgramController(ICalculator calculator)
    {
        _calculator = calculator;
    }
    public void Run()
    {
        Console.WriteLine("Please write the sum to calculate");
        Console.WriteLine("type 'q' to quit");
        var input = Console.ReadLine();
        // Calculator calculator = new ();
        while (input != "q")
        {
            Console.WriteLine("---------------------");
            var result = _calculator.Calculate(input);
            Console.WriteLine(result);
            Console.WriteLine("---------------------");
            Console.WriteLine("Please write the sum to calculate");
            Console.WriteLine("type 'q' to quit");
            input = Console.ReadLine();
        }
    }
}