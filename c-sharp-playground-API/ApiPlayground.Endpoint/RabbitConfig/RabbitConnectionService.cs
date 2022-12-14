using RabbitMQ.Client;

namespace ApiPlayground.RabbitConfig;

public class RabbitConnectionService : IRabbitConnectionService
{
    public IConnection CreateChannel()
    {
        ConnectionFactory connection = new ConnectionFactory() {HostName = "localhost"};
        connection.DispatchConsumersAsync = true;
        var channel = connection.CreateConnection();
        return channel;
    }
}