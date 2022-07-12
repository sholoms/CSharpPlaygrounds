using System;

namespace CSPlayground.services;

public class Calculator : ICalculator
{
    private readonly IStringParsingService _parser;
    public Calculator(IStringParsingService parser)
    {
        this._parser = parser;
    }
    public string Calculate(string input)
    {
        try
        {
            var result = ""; 
            var data = _parser.ParseStringToCalculations(input);
            while (data.Count > 2)
            {
                result = PerformCalculation(data[1]);
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

    private string PerformCalculation(string data)
    {
        var calculation = _parser.ParseStringToSingleCalculation(data);
        var firstNum = Int32.Parse(calculation[1]);
        var secondNum = Int32.Parse(calculation[3]);
        var result = calculation[2] switch
        {
            "+" => firstNum + secondNum,
            "-" => firstNum - secondNum,
            "*" => firstNum * secondNum,
            "/" => firstNum / secondNum,
            _ => throw new ArgumentException()
        };

        return result.ToString();
        
    }
}