using ApiPlayground.Handlers;

namespace ApiPlayground.services;

public class ConsumerHostedService : BackgroundService
{
    private readonly IRabbitMessageHandler _consumerService;

    public ConsumerHostedService(IRabbitMessageHandler consumerService)
    {
        _consumerService = consumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumerService.ReadMessgaes();
    }
}