using RabbitMQ.Client;

namespace ApiPlayground.RabbitConfig;

public interface IRabbitConnectionService
{
    IConnection CreateChannel();
}