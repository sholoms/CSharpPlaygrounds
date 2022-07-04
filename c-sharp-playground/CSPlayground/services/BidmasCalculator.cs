namespace CSharpPlayground.services;

public class BidmasCalculator : ICalculator
{
    private readonly IStringParsingService _parser;

    public BidmasCalculator(IStringParsingService parser)
    {
        _parser = parser;
    }

    public string Calculate(string input)
    {
        if (input.StartsWith('(') && input.EndsWith(')'))
        {
            input = input.Substring(1, input.Length - 2);
        }
        var nextCalculation = _parser.nextCalculation(input);
        while (nextCalculation != input)
        {
            input = input.Replace(nextCalculation, Calculate(nextCalculation));
            nextCalculation = _parser.nextCalculation(input);
        }
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
            return result;
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