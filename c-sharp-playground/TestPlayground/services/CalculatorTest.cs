using CSharpPlayground.services;
using Shouldly;
using TestStack.BDDfy;
using Xunit;
using NSubstitute;

namespace TestPlayground.services;

public class CalculatorTest
{
    private readonly Calculator _calculator = new(Substitute.For<IStringParsingService>());
    private string _string;
    private string _result;
    
    [Theory]
    [InlineData("3 + 5", "Result: 8")]
    [InlineData("5 - 3", "Result: 2")]
    [InlineData("3 * 5", "Result: 15")]
    [InlineData("15 / 3", "Result: 5")]
    public void CalculateShouldReturnTheCorrectResponseWhnGiveAValidString(string data, string response)
    {
        this.Given(x => GivenAString(data))
            .When(x => WhenCalculatingAnEquation())
            .Then(x => ThenItShouldReturnTheCorrectString(response))
            .BDDfy();
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