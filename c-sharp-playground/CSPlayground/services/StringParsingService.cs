using System.Text.RegularExpressions;

namespace CSharpPlayground.services;

public class StringParsingService : IStringParsingService
{
    public List<string> ParseStringToCalculations(string input)
    {
        var match = Constants.CompleteRegex.Match(input);
        if (match.Success)
        {
            return ShowMatches(Constants.CompleteRegex, match);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public List<string> ParseStringToCalculation(string input)
    {
        var match = Constants.CalculationRegex.Match(input);
        if (match.Success)
        {
            return ShowMatches(Constants.CalculationRegex, match);
        }
        else
        {
            throw new ArgumentException();
        }
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
