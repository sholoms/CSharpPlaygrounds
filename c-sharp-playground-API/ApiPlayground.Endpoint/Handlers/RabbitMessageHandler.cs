using ApiPlayground.controllers;
using ApiPlayground.RabbitConfig;
using ApiPlayground.services;
using ApiPlayground.services.interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiPlayground.Handlers;


public class RabbitMessageHandler : IRabbitMessageHandler
{
    private readonly IFileService _fileService;
    private readonly IRabbitMessageSender _publisher;
    private readonly IBidmasCalculator _calculator;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly string _queueName;

    public RabbitMessageHandler(IRabbitConnectionService rabbitMqService, IFileService fileService, IRabbitMessageSender publisher, IBidmasCalculator calculator)
    {
        _fileService = fileService;
        _publisher = publisher;
        _calculator = calculator;
        _connection = rabbitMqService.CreateChannel();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare("calculator-exchange", ExchangeType.Direct, true, false, null);
        _queueName = "RabbitCalculator";
        var deadLetterManager = new DeadLetterManager(_queueName);
        _channel.QueueDeclare(_queueName, true, true, false, new Dictionary<string, object> { 
            { "x-dead-letter-exchange", deadLetterManager.SourceQueueDeadletterExchange }, 
            { "x-dead-letter-routing-key", deadLetterManager.SourceQueueDeadletterRoutingKey }
        });
        deadLetterManager.ConfigureDeadletterRouting(_connection);
        _channel.QueueBind(queue: _queueName,
            exchange: "calculator-exchange",
            routingKey: "request");
    }
    public Task ReadMessgaes()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var text = System.Text.Encoding.UTF8.GetString(body);
            var request = JsonConvert.DeserializeObject<AddToStorageRequest>(text);
            Console.WriteLine(request);
            await _fileService.WriteFile(request);
            try
            {
                foreach (var requestCalculation in request.Calculations)
                {
                    _publisher.Send("logs", _calculator.Calculate(requestCalculation));
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            }
            catch
            {
                Console.WriteLine("message rejected");
                _channel.BasicReject(ea.DeliveryTag, false);
            }
            
        };
        _channel.BasicConsume(_queueName, false, consumer);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
            _channel.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}