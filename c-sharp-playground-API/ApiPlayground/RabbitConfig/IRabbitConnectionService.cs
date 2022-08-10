using RabbitMQ.Client;

namespace ApiPlayground.services;

public interface IRabbitConnectionService
{
    IConnection CreateChannel();
}