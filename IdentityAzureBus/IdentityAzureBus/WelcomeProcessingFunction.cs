using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace IdentityAzureBus
{
    //public class WelcomeProcessingFunction
    //{
    //    private readonly ILogger<WelcomeProcessingFunction> _logger;

    //    public WelcomeProcessingFunction(ILogger<WelcomeProcessingFunction> logger)
    //    {
    //        _logger = logger;
    //    }

    //    [Function(nameof(WelcomeProcessingFunction))]
    //    public async Task Run(
    //        [ServiceBusTrigger("order.created", "order.processing", Connection = "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;")]
    //        ServiceBusReceivedMessage message,
    //        ServiceBusMessageActions messageActions)
    //    {
    //        _logger.LogInformation("Message ID: {id}", message.MessageId);
    //        _logger.LogInformation("Message Body: {body}", message.Body);
    //        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

    //        // Complete the message
    //        await messageActions.CompleteMessageAsync(message);
    //    }
    //}
}
