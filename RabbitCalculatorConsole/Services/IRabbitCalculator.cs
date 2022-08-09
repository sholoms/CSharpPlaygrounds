namespace RabbitCalculatorConsole.Services;

public interface IRabbitCalculator
{
    Task<string> Calculate(string input);
    void close();
}