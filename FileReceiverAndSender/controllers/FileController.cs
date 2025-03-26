using FileReceiverAndSender.Services;

namespace FileReceiverAndSender.controllers;

public class FileController : IProgramController
{
    private readonly IRabbitCalculator _rabbitFileWriter;

    public FileController(IRabbitCalculator rabbitFileWriter)
    {
        _rabbitFileWriter = rabbitFileWriter;
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
            //var result = _calculator.Calculate(input);
            var result = await _rabbitFileWriter.send(input);
            Console.WriteLine(result);
            Console.WriteLine("---------------------");
            Console.WriteLine("Please write the sum to calculate");
            Console.WriteLine("type 'q' to quit");
            input = Console.ReadLine();
        }

        _rabbitFileWriter.close();
    }
}