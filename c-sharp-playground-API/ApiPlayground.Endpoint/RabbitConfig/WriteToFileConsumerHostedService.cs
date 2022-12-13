using ApiPlayground.Handlers;

namespace ApiPlayground.RabbitConfig;

public class WriteToFileConsumerHostedService : BackgroundService
{
    private readonly IRabbitMessageHandler _writeToFileConsumerHostedService;

    public WriteToFileConsumerHostedService(IRabbitMessageHandler writeToFileConsumerHostedService)
    {
        _writeToFileConsumerHostedService = writeToFileConsumerHostedService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _writeToFileConsumerHostedService.ReadMessgaes();
    }
}