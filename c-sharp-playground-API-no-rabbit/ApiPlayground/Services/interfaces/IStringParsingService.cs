namespace ApiPlayground.services.interfaces;

public interface IStringParsingService
{
    List<string> ParseStringToCalculations(string input);
    List<string> ParseStringToSingleCalculation(string input);
    string NextCalculation(string input);
}