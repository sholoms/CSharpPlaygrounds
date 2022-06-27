namespace CSharpPlayground.services;

public interface IStringParsingService
{
    List<string> ParseStringToCalculations(string input);
    List<string> ParseStringToCalculation(string input);
}