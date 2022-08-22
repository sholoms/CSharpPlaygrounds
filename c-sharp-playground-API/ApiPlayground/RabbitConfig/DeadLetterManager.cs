using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiPlayground.RabbitConfig;

public class DeadLetterManager
{
    private const int DL_RETRY_COUNT = 5;
    private const int DL_RETRY_INTERVAL_MILLISECONDS = 5000;
    private const string DL_RETRY_HEADER_NAME = "dl-retry-count";

    private readonly string sourceQueueName;
    private readonly string deadletterRoutingQueueName;
    private readonly string deadletterScheduledQueueName;
    private readonly string deadletterQueueName;

    public string SourceQueueDeadletterExchange => "";
    public string SourceQueueDeadletterRoutingKey => deadletterRoutingQueueName;

    private IModel channel;

    //sourceQueue --(DL)-->sourceQueue.deadletter.routing---[retry limit?]---[yes]--->sourceQueue.deadletter
    //   ^                                                      |
    //   |                                                      |
    //  (DL after expiry)                                      [no]
    //   |                                                      |
    //   |------- sourceQueue.deadletter.retryscheduled <-------|

    public DeadLetterManager(string sourceQueueName)
    {
        this.sourceQueueName = sourceQueueName;
        this.deadletterRoutingQueueName = $"{sourceQueueName}.DL.Routing";
        this.deadletterScheduledQueueName = $"{sourceQueueName}.DL.ScheduledRetry";
        this.deadletterQueueName = $"{sourceQueueName}.DL";
    }

    public void ConfigureDeadletterRouting(IConnection connection)
    {
        channel = connection.CreateModel();

        // Routing queue- increments retry count, and routes to either deadletter or scheduled retry queue depending on retry count
        channel.QueueDeclare(deadletterRoutingQueueName, true, false, false);

        // Scheduled Retry queue- messages wait for 5 seconds, and then an published back to the normal queue
        channel.QueueDeclare(deadletterScheduledQueueName, true, false, false, new Dictionary<string, object>
        {
            {"x-dead-letter-exchange", ""},
            {"x-dead-letter-routing-key", this.sourceQueueName},
            {"x-message-ttl", DL_RETRY_INTERVAL_MILLISECONDS}
        });

        // Proper dead letter queue
        channel.QueueDeclare(deadletterQueueName, true, false, false);

        var dlRoutingConsumer = new AsyncEventingBasicConsumer(channel);
        dlRoutingConsumer.Received += async (sender, ea) => dlRoutingMessageHandler(sender, ea, channel);
        channel.BasicConsume(queue: deadletterRoutingQueueName, autoAck: false, consumer: dlRoutingConsumer);
    }

    private Task dlRoutingMessageHandler(object sender, BasicDeliverEventArgs ea, IModel channel)
    {
        if (channel == null)
        {
            throw new Exception("Deadletter queue not configured correctly.");
        }

        var message = Encoding.UTF8.GetString(ea.Body.ToArray());

        // Get retry count from custom header
        var retryCount = GetRetryCount(ea);
        retryCount++;

        string targetQueue;

        if (retryCount >= 5)
        {
            // Already been tried 5 times, send it to the dead letter queue
            Console.WriteLine($"DL Routing consumer. Message: {message}. Retry count:{retryCount}. Deadlettering");

            targetQueue = this.deadletterQueueName;
        }
        else
        {
            // Not yet tried 5 times, send it to the retry queue
            Console.WriteLine($"DL Routing consumer. Message: {message}. Retry count:{retryCount}. Retrying");

            targetQueue = this.deadletterScheduledQueueName;
        }

        // Set retry count from custom header
        var msgProperties = channel.CreateBasicProperties();
        msgProperties.Headers = new Dictionary<string, object> {{DL_RETRY_HEADER_NAME, retryCount}};

        channel.BasicAck(ea.DeliveryTag, false);
        channel.BasicPublish("", targetQueue, false, msgProperties, ea.Body);
        return Task.CompletedTask;
    }

    private int GetRetryCount(BasicDeliverEventArgs ea)
    {
        object retryCount;
        if (ea.BasicProperties.Headers.TryGetValue(DL_RETRY_HEADER_NAME, out retryCount))
        {
            return (int) retryCount;
        }

        return 0;
    }
}