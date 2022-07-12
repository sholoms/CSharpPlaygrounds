using System.Collections.Generic;
using System.Linq;
using ApiPlayground.services;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using NSubstitute;

namespace TestPlayground.services;

public class CalculatorTest
{
    private readonly IStringParsingService _stringParser = Substitute.For<IStringParsingService>();
    private readonly LeftToRightCalculator _calculator;

    public CalculatorTest()
    {
        _calculator = new LeftToRightCalculator(_stringParser);
    }
    private string _string;
    private string _result;
    
    [Theory]
    [InlineData("3+5", "Result: 8", new[] { "3+5", "3+5", "", "" }, new[] { "3+5", "3", "+", "5" })]
    [InlineData("5-3", "Result: 2", new[] { "5-3", "5-3", "", "" }, new[] { "5-3", "5", "-", "3" })]
    [InlineData("3*5", "Result: 15", new[] { "3*5", "3*5", "", "" }, new[] { "3*5", "3", "*", "5" })]
    [InlineData("15/3", "Result: 5", new[] { "15/3", "15/3", "", "" }, new[] { "15/3", "15", "/", "3" })]
    public void CalculateShouldReturnTheCorrectResponseWhnGiveAValidStringWithASingleCalculation(
        string data, string response, string[] calculationsResponse, string[] singleCalculationResponse)
    {
        this.Given(x => GivenAString(data))
            .And(x => TheCalculationsParserWillReturn(calculationsResponse))
            .And(x=> TheSingleCalculationsParserWillReturn(singleCalculationResponse))
            .When(x => WhenCalculatingAnEquation())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
    }
    //
    // [Theory]
    // [InlineData("3+5", "Result: 8", new[] { "3+5", "3+5", "" }, new[] { "3+5", "3", "+", "5" })]
    // [InlineData("5-3", "Result: 2", new[] { "5-3", "5-3", "" }, new[] { "5-3", "5", "-", "3" })]
    // [InlineData("3*5", "Result: 15", new[] { "3*5", "3*5", "" }, new[] { "3*5", "3", "*", "5" })]
    // [InlineData("15/3", "Result: 5", new[] { "15/3", "15/3", "" }, new[] { "15/3", "15", "/", "3" })]
    // public void CalculateShouldReturnTheCorrectResponseWhnGiveAValidStringWithMultipleCalculation(
    //     string data, string response, string[] calculationsResponse, string[] singleCalculationResponse)
    // {
    //     this.Given(x => GivenAString(data))
    //         .And(x => TheCalculationsParserWillReturn(calculationsResponse))
    //         .And(x=> TheSingleCalculationsParserWillReturn(singleCalculationResponse))
    //         .When(x => WhenCalculatingAnEquation())
    //         .Then(x => ThenItShouldReturnTheCorrectString(response))
    //         .BDDfy();
    // }

    private void TheSingleCalculationsParserWillReturn(string[] singleCalculationResponse)
    {
        var list = singleCalculationResponse.ToList();
        _stringParser.ParseStringToSingleCalculation(Arg.Any<string>()).Returns(list);
    }

    private void TheCalculationsParserWillReturn(string[] calculationsResponse)
    {
         var list = calculationsResponse.ToList();
        _stringParser.ParseStringToCalculations(Arg.Any<string>()).Returns(list);
    }

    private void ThenItShouldReturnTheCorrectString(string response)
    {
        _result.ShouldBe(response);
    }

    private void WhenCalculatingAnEquation()
    {
        _result = _calculator.Calculate(_string);
    }

    private void GivenAString(string data)
    {
        _string = data;
    }
}