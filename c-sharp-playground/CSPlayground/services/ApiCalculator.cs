using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSPlayground.services;

public class ApiCalculator : IApiCalculator
{
    private readonly IApiCalculatorClient _client;
    public ApiCalculator(IApiCalculatorClient client)
    {
        _client = client;
    }
    public async Task<string> Calculate(string input)
    {

        var body = new CalculationRequest()
        {
            Calculation = input,
            Ltr = false
        };
        var response = await  _client.CalculatePostAsync(body);
        return response.Result;
    }

    public async Task<string> FileResults()
    {
        var response = await _client.CalculateFileAsync();
        var stringResult = new StringBuilder();
        foreach (var result in response.Results )
        {
            stringResult.Append($"{result.Result}\n");
        }

        return stringResult.ToString();
    }
}