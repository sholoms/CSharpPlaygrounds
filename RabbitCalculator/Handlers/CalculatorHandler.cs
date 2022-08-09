using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitCalculator.Handlers;

public class CalculatorHandler : ICalculatorHandler
{
    private readonly IApiCalculatorClient _apiCalculator;

    public CalculatorHandler(IApiCalculatorClient apiCalculator)
    {
        _apiCalculator = apiCalculator;
    }
    public async Task Run()
    {
        var factory = new ConnectionFactory() {HostName = "localhost"};
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "calculator_rpc_queue", durable: false,
                exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "calculator_rpc_queue",
                autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");
            consumer.Received += async (model, ea) =>
            {
                string response = null;
                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;
                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"received: {message}");
                    var requestBody = new CalculationRequest()
                    {
                        Calculation = message,
                        Ltr = false
                    };
                    var calculatorResponse = await _apiCalculator.CalculatePostAsync(requestBody);
                    response = calculatorResponse.Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                        basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                        multiple: false);
                }
            };

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}