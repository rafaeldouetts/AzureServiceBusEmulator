using Azure.Messaging.ServiceBus;
using IdentityAzureBus.Common;
using IdentityAzureBus.Domain.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IdentityAzureBus.Functions.Functions
{
    public class WelcomeWhatsappProcessingFunction
    {
        private readonly ILogger<WelcomeWhatsappProcessingFunction> _logger;
        private readonly IWhatsappService _whatsappService;
        public WelcomeWhatsappProcessingFunction(ILogger<WelcomeWhatsappProcessingFunction> logger, IWhatsappService whatsappService)
        {
            _logger = logger;
            _whatsappService = whatsappService;
        }

        [Function(nameof(WelcomeWhatsappProcessingFunction))]
        public async Task Run(
            [ServiceBusTrigger("client.created", "welcome.whatsapp", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            try
            {
                _logger.LogInformation("Message ID: {id}", message.MessageId);
                _logger.LogInformation("Message Body: {body}", message.Body);
                _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

                var user = JsonConvert.DeserializeObject<User>(message.Body.ToString());

                throw new Exception("Erro ao realizar o disparo de mensagem");

                //await _whatsappService.AdicionarNumero(user.Telefone);

                await _whatsappService.EnviarMensagem(user.Telefone, "hello_world");

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
