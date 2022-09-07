namespace ApiPlayground.Helpers;

public class CalculatorHelper : ICalculatorHelper
{
    public string PerformCalculation(List<string> calculation)
    {
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