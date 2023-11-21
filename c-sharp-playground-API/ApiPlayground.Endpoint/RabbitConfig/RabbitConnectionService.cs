using ApiPlayground.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ApiPlayground.RabbitConfig;

public class RabbitConnectionService : IRabbitConnectionService
{
    public IConnection CreateChannel(IOptions<RabbitSettings> config)
    {
        ConnectionFactory connection = new ConnectionFactory() {HostName = config.Value.Host, Port = config.Value.Port};
        connection.DispatchConsumersAsync = true;
        var channel = connection.CreateConnection();
        return channel;
    }
}