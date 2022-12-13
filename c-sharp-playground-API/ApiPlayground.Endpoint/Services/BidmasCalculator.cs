using ApiPlayground.Helpers;
using ApiPlayground.services.interfaces;

namespace ApiPlayground.services;

public class BidmasCalculator : IBidmasCalculator
{
    private readonly IStringParsingService _parser;
    private readonly ICalculatorHelper _calculator;

    public BidmasCalculator(IStringParsingService parser, ICalculatorHelper calculator)
    {
        _parser = parser;
        _calculator = calculator;
    }

    public string Calculate(string input)
    {
        if (input.StartsWith('(') && input.EndsWith(')'))
        {
            input = input.Substring(1, input.Length - 2);
        }

        var nextCalculation = _parser.NextCalculation(input);
        while (nextCalculation != input)
        {
            input = input.Replace(nextCalculation, Calculate(nextCalculation));
            nextCalculation = _parser.NextCalculation(input);
        }

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

        return result;

    }
}