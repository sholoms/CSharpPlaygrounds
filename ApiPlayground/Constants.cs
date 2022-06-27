using System.Text.RegularExpressions;

namespace ApiPlayground;

public static class Constants
{
    public static readonly Regex CompleteRegex = new Regex("^([0-9]+[+\\-*/][0-9]+)([+\\-*/][0-9]+)*$");
    public static readonly Regex CalculationRegex = new Regex("^([0-9]+)([+\\-*/])([0-9]+)$");

}