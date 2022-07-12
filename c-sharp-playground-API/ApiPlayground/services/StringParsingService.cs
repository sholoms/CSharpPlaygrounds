using System.Text.RegularExpressions;

namespace ApiPlayground.services;

public class StringParsingService : IStringParsingService
{
    public List<string> ParseStringToCalculations(string input)
    {
        var match = Constants.CompleteRegex.Match(input);
        if (match.Success)
        {
            return ShowMatches(Constants.CompleteRegex, match);
        }
        throw new ArgumentException();
    }

    public List<string> ParseStringToSingleCalculation(string input)
    {
        var match = Constants.CalculationRegex.Match(input);
        if (match.Success)
        {
            return ShowMatches(Constants.CalculationRegex, match);
        }
        throw new ArgumentException();
    }

    public string NextCalculation(string input)
    {

        var match = Constants.BracketsRegex.Match(input);
        if (match.Success)
        {
            return ShowMatches(Constants.BracketsRegex, match).Last();
        }

        match = Constants.MultiplyDivideRegex.Match(input);
        if (match.Success)
        {
            return ShowMatches(Constants.MultiplyDivideRegex, match).First();
        }
        
        return input;
    }

    private static List<string> ShowMatches(Regex r, Match m)
    {
        List<string> groups = new List<string>();
        string[] names = r.GetGroupNames();
        foreach (var name in names)
        {
            groups.Add(m.Groups[name].Value);
        }

        return groups;
    }
}
