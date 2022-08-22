using System.Text;
using Newtonsoft.Json;
using RabbitCalculatorConsole.Services;
using RabbitMQ.Client;
using WriteToFileRabbitConsole.Models;

namespace WriteToFileRabbitConsole.Services;

public class RabbitCalculator : IRabbitCalculator
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IBasicProperties _properties;

    public RabbitCalculator()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _properties = _channel.CreateBasicProperties();
        _properties.Persistent = true;
        _channel.ExchangeDeclare("calculator-exchange", ExchangeType.Direct, true, false, null);
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }

    public Task<string> send(string input)
    {

        var jsonString = JsonConvert.SerializeObject(new AddToFileRequest {Calculations = new List<string>(){input}});
        var body = Encoding.UTF8.GetBytes(jsonString);

        _channel.BasicPublish(exchange: "calculator-exchange",
            routingKey: "request",
            basicProperties: _properties,
            body: body);

        return Task.FromResult("sent succesfuly");
    }

    public void close()
    {
        _connection.Close();
    }

    public void Close()
    {
        _connection.Close();
    }
}