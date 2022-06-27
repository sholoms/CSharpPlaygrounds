using System.Collections.Generic;
using System.Linq;
using CSharpPlayground.services;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using NSubstitute;


namespace TestPlayground.services;

public class StringParserTest
{
    private IStringParsingService _stringParser = new StringParsingService();
    private string _input;
    private List<string> _response;
    
    [Theory]
    [InlineData("3+5", new[] { "3+5", "3", "+", "5" })]
    [InlineData("5-3", new[] { "5-3", "5", "-", "3" })]
    [InlineData("3*5", new[] { "3*5", "3", "*", "5" })]
    [InlineData("15/3", new[] { "15/3", "15", "/", "3" })]
    public void ParseSingleCalculationShouldReturnTheCorrectResponseWhenGiveAValidStringWithASingleCalculation(
        string input, string[] response)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingASingleCalculation())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }
    
    [Theory]
    [InlineData("3+5", new[] { "3+5", "3+5", "", "" })]
    [InlineData("5-3", new[] { "5-3", "5-3", "", "" })]
    [InlineData("3*5", new[] { "3*5", "3*5", "", "" })]
    [InlineData("15/3", new[] { "15/3", "15/3", "", "" })]
    public void ParseCalculationsShouldReturnTheCorrectResponseWhenGiveAValidStringWithASingleCalculation(
        string input, string[] response)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingACalculation())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }
    
        
    [Theory]
    [InlineData("3+5-8", new[] { "3+5-8", "3+5", "-8", "" })]
    [InlineData("5-3*3", new[] { "5-3*3", "5-3", "*3", "" })]
    [InlineData("3*5/9+5", new[] { "3*5/9+5", "3*5", "/9", "+5" })]
    [InlineData("15/3+195*3", new[] { "15/3+195*3", "15/3", "+195", "*3" })]
    public void ParseCalculationsShouldReturnTheCorrectResponseWhenGiveAValidStringWithMultipleCalculations(
        string input, string[] response)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingACalculation())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }


    private void WhenParsingACalculation()
    {
        _response = _stringParser.ParseStringToCalculations(_input);
    }


    private void ThenItShouldReturnTheCorrectString(string[] response)
    {
        _response.ShouldBe(response);
    }

    private void WhenParsingASingleCalculation()
    {
        _response = _stringParser.ParseStringToSingleCalculation(_input);
    }

    private void GivenAString(string input)
    {
        _input = input;
    }
}