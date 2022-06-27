namespace ApiPlayground.Services;

public class CalculatorService : ICalculatorService
{
    private readonly IStringParsingService _parser;
    public CalculatorService(IStringParsingService parser)
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
                data[1] = result;
                if (string.IsNullOrEmpty(data[2]))
                {
                    data.RemoveAt(2);
                }
                else
                {
                    data.RemoveAt(0);
                    data = _parser.ParseStringToCalculations(string.Join("", data));
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
        var calculation = _parser.ParseStringToCalculation(data);
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