namespace RabbitCalculatorConsole.Services;

public interface IRabbitCalculator
{
    Task<string> send(string input);
    void close();
}