namespace ApiPlayground.Extensions;

public static class StringCalculatorExtensions
{
    public static string RemoveSurroundedByBrackets(this string str)
    {
        var result = str;
        if (str.StartsWith('(') && str.EndsWith(')'))
        {
            result = str.Substring(1, str.Length - 2);
        }
        return result;
    }
}