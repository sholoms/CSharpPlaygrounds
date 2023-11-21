using System.Text;
using ApiPlayground.Configuration;
using ApiPlayground.Handlers;
using ApiPlayground.services.interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiPlayground.RabbitConfig;


public class RabbitMessageSender : IRabbitMessageSender
{
    private readonly IRabbitConnectionService _rabbitMqService;
    private readonly IOptions<RabbitSettings> _config;

    public RabbitMessageSender(IRabbitConnectionService rabbitMqService, IOptions<RabbitSettings> config)
    {
        _rabbitMqService = rabbitMqService;
        _config = config;
    }

    public void Send(string routingKey, string message)
    {
        using var connection = _rabbitMqService.CreateChannel(_config);
        using var model = connection.CreateModel();
        var body = Encoding.UTF8.GetBytes(message);
        model.BasicPublish("calculator-exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}