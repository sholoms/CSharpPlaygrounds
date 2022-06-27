using System;
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
    private Exception _exception;
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
            .When(x => WhenParsingACalculations())
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
            .When(x => WhenParsingACalculations())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }

    [Theory]
    [InlineData("3+5qw8")]
    [InlineData("3 + 8")]
    [InlineData("hello")]
    [InlineData("3*5/9+5qwe")]
    [InlineData("asd15/3+195*3")]
    public void ParseCalculationsShouldThrowAnErrorForAnInvalidString(string input)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingACalculations())
            .Then(x => ThenItShouldThrowAnError())
            .BDDfy();
    }

    private void ThenItShouldThrowAnError()
    {
        _exception.ShouldNotBeNull();
        _exception.ShouldBeOfType(typeof(ArgumentException));
    }

    private void WhenParsingACalculations()
    {
        try
        {
            _response = _stringParser.ParseStringToCalculations(_input);
        }
        catch (Exception e)
        {
            _exception = e;
        }
        
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