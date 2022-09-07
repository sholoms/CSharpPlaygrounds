using System;
using System.Collections.Generic;
using System.Linq;
using ApiPlayground.services;
using ApiPlayground.services.interfaces;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using NSubstitute;


namespace TestPlayground.services;

public class StringParserTest
{
    private readonly IStringParsingService _stringParser = new StringParsingService();
    private string _input;
    private Exception _exception;
    private List<string> _listResponse;
    private string _stringResponse;
    
    [Theory]
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
    [InlineData("3+5", new[] { "3+5", "3+5", "" })]
    public void ParseCalculationsShouldReturnTheCorrectResponseWhenGiveAValidStringWithASingleCalculation(
        string input, string[] response)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingACalculations())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }
    
        
    [Theory]
    [InlineData("3+5-8", new[] { "3+5-8", "3+5", "-8"})]
    [InlineData("3+5-8+5", new[] { "3+5-8+5", "3+5", "+5"})]
    public void ParseCalculationsShouldReturnTheCorrectResponseWhenGiveAValidStringWithMultipleCalculations(
        string input, string[] response)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingACalculations())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }

    
    [Theory]
    [InlineData("3+5*8", "5*8")]
    [InlineData("3+5/8*5",  "5/8")]
    [InlineData("3+5/8*(5+3)",  "(5+3)")]
    public void ParseNextCalculationsShouldReturnTheCorrectResponseWhenGivenAValidString(
        string input, string response)
    {
        this.Given(x => GivenAString(input))
            .When(x => WhenParsingACalculationForNextCalculation())
            .Then(x => ThenItShouldReturnTheCorrectCalculation(response))
            .BDDfy();
    }

    [Theory]
    [InlineData("3+5qw8")]
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
            _listResponse = _stringParser.ParseStringToCalculations(_input);
        }
        catch (Exception e)
        {
            _exception = e;
        }
        
    }

    private void WhenParsingACalculationForNextCalculation()
    {
        try
        {
            _stringResponse = _stringParser.NextCalculation(_input);
        }
        catch (Exception e)
        {
            _exception = e;
        }
    }

    private void ThenItShouldReturnTheCorrectCalculation(string response)
    {
        _stringResponse.ShouldBe(response);
    }


    private void ThenItShouldReturnTheCorrectString(string[] response)
    {
        _listResponse.ShouldBe(response);
    }

    private void WhenParsingASingleCalculation()
    {
        _listResponse = _stringParser.ParseStringToSingleCalculation(_input);
    }

    private void GivenAString(string input)
    {
        _input = input;
    }
}