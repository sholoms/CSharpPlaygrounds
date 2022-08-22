using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitLogger;

class RabbitLogger
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare("calculator-exchange", ExchangeType.Direct, true, false, null);
            var queueName = "rabbitLogger";
            channel.QueueDeclare(exclusive: false, queue: queueName);
            channel.QueueBind(queue: queueName,
                exchange: "calculator-exchange",
                routingKey: "logs");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                // Console.WriteLine(" [x] Done");
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}