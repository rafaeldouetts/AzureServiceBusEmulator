using Azure.Messaging.ServiceBus;
using IdentityAzureBus.Common;
using IdentityAzureBus.Domain.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IdentityAzureBus.Functions.Functions
{
    public class WelcomeProcessingFunction
    {
        private readonly ILogger<WelcomeProcessingFunction> _logger;
        private readonly IEmailService _emailService;
        public WelcomeProcessingFunction(ILogger<WelcomeProcessingFunction> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [Function(nameof(WelcomeProcessingFunction))]
        public async Task Run(
            [ServiceBusTrigger("client.created", "welcome.email", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            try
            {
                _logger.LogInformation("Message ID: {id}", message.MessageId);
                _logger.LogInformation("Message Body: {body}", message.Body);
                _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

                var user = JsonConvert.DeserializeObject<User>(message.Body.ToString());

                throw new Exception("Erro ao realizar o disparo de email");

                //notificar via email
                await _emailService.SendEmailAsync(user.Email, user.Name, "welcome", "textContent", "<html><body><h1>Seja bem vindo </h1></body></html>");

                // Complete the message
                await messageActions.CompleteMessageAsync(message);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
