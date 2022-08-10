namespace ApiPlayground.Handlers;

public interface IRabbitMessageHandler
{
    Task ReadMessgaes();
    void Dispose();
}