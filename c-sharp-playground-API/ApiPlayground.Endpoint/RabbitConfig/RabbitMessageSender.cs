using System.Text;
using ApiPlayground.Handlers;
using ApiPlayground.services.interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiPlayground.RabbitConfig;


public class RabbitMessageSender : IRabbitMessageSender
{
    private readonly IRabbitConnectionService _rabbitMqService;

    public RabbitMessageSender(IRabbitConnectionService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    public void Send(string routingKey, string message)
    {
        using var connection = _rabbitMqService.CreateChannel();
        using var model = connection.CreateModel();
        var body = Encoding.UTF8.GetBytes(message);
        model.BasicPublish("calculator-exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}