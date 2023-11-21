using ApiPlayground.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ApiPlayground.RabbitConfig;

public interface IRabbitConnectionService
{
    IConnection CreateChannel(IOptions<RabbitSettings> config);
}