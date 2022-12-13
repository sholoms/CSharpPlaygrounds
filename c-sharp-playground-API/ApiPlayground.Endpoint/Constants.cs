using System.Text.RegularExpressions;

namespace ApiPlayground;

public static class Constants
{
    public static readonly Regex CompleteRegex = new("^([0-9]+[\\+\\-\\*\\/][0-9]+)([\\+\\-\\*\\/][0-9]+)*$");
    public static readonly Regex CalculationRegex = new ("^([0-9]+)([+\\-\\*/])([0-9]+)$");
    public static readonly Regex BracketsRegex = new("\\([^\\(\\)]*\\)");
    public static readonly Regex MultiplyDivideRegex = new("[0-9]*[\\*\\/][0-9]*");
}