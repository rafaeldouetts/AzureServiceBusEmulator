using Azure.Messaging.ServiceBus;
using IdentityAzureBus.Domain.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace IdentityAzureBus.Functions.Functions
{
    public class DeadLetterProcessFunction
    {
        private readonly ILogger<DeadLetterProcessFunction> _logger;
        private readonly IMongoDBService _mongoDBService;
        public DeadLetterProcessFunction(ILogger<DeadLetterProcessFunction> logger, IMongoDBService mongoDBService)
        {
            _logger = logger;
            _mongoDBService = mongoDBService;
        }

        [Function(nameof(DeadLetterProcessFunction))]
        public async Task Run(
            [ServiceBusTrigger("DeadLetterQueue", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Verificar se a mensagem é da DeadLetterQueue
            if (message.DeadLetterReason != null)
            {
                _logger.LogError("Dead Letter Reason: {deadLetterReason}", message.DeadLetterErrorDescription);
            }

            await _mongoDBService.SaveFailedMessageAsync(message.MessageId, message.DeadLetterErrorDescription, message.Body.ToString());

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
