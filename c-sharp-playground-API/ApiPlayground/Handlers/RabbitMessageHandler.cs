using ApiPlayground.services;
using ApiPlayground.services.interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiPlayground.Handlers;


public class RabbitMessageHandler : IRabbitMessageHandler
{
    private readonly IFileService _fileService;
    private readonly IModel _model;
    private readonly IConnection _connection;
    private readonly string _queueName;

    public RabbitMessageHandler(IRabbitConnectionService rabbitMqService, IFileService fileService)
    {
        _fileService = fileService;
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();
        _model.ExchangeDeclare("topic_logs", ExchangeType.Topic);
        _queueName = _model.QueueDeclare().QueueName;
        _model.QueueBind(queue: _queueName,
            exchange: "topic_logs",
            routingKey: "#");
    }
    public async Task ReadMessgaes()
    {
        var consumer = new AsyncEventingBasicConsumer(_model);
        consumer.Received += async (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var text = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine(text);
            await _fileService.WriteFile(text);
            _model.BasicAck(ea.DeliveryTag, false);
        };
        _model.BasicConsume(_queueName, false, consumer);
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}