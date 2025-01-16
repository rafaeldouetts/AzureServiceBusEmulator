using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IdentityAzureBus.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Aqui você pode reagir ao evento, por exemplo, enviar um e-mail ou logar informações.
            _logger.LogInformation($"User Created: Username = {notification.Username}, Email = {notification.Email}");
            
            var client = new ServiceBusClient("Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;");
            var sender = client.CreateSender("client.created");
            await sender.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(notification))));

            await Task.CompletedTask;
        }
    }
}
