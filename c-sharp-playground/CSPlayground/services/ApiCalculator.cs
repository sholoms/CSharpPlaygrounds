using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CSPlayground.Models;
using Newtonsoft.Json;

namespace CSPlayground.services;

public class ApiCalculator : IApiCalculator
{
    private readonly HttpClient _client;
    public ApiCalculator(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://localhost:9028/");
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
    }
    public async Task<string> Calculate(string input)
    {

        var body = new CalculationRequest()
        {
            Calculation = input,
            Ltr = false
        };
        var httpResponse = await _client.PostAsJsonAsync("calculate", body);
        var response = JsonConvert.DeserializeObject<CalculationResponse>(await httpResponse.Content.ReadAsStringAsync());
        return response.Result;
    }

    public async Task GetFileResults()
    {
        var httpResponse = await _client.GetAsync("file/results");
        var response = JsonConvert.DeserializeObject<FileResultResponse>(await httpResponse.Content.ReadAsStringAsync());
    }
}