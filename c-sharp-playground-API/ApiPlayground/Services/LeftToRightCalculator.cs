using ApiPlayground.Helpers;
using ApiPlayground.services.interfaces;

namespace ApiPlayground.services;

public class LeftToRightCalculator : ILeftToRightCalculator
{
    private readonly IStringParsingService _parser;
    private readonly ICalculatorHelper _calculator;

    public LeftToRightCalculator(IStringParsingService parser, ICalculatorHelper calculator)
    {
        this._parser = parser;
        _calculator = calculator;
    }
    public string Calculate(string input)
    {
        try
        {
            var result = ""; 
            var data = _parser.ParseStringToCalculations(input);
            while (data.Count > 2)
            {
                var calculation = _parser.ParseStringToSingleCalculation(data[1]);
                result = _calculator.PerformCalculation(calculation);
                if (string.IsNullOrEmpty(data[2]))
                {
                    data.RemoveAt(2);
                }
                else
                {
                    var newCalculations = result + data[0][data[1].Length..];
                    data = _parser.ParseStringToCalculations(newCalculations);
                }
            }
            return $"Result: {result}";
        }
        catch (Exception)
        {
            return "invalid input";
        }
    }


}