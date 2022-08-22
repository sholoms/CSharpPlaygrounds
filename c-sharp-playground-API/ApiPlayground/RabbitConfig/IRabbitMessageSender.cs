namespace ApiPlayground.RabbitConfig;

public interface IRabbitMessageSender
{
    void Send(string routingKey, string message);
}