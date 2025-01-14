using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Caso queira realizar alguma lógica assíncrona (ex: enviar e-mail)
            // await SomeAsyncService.SendEmailAsync(notification.Email);

            await Task.CompletedTask;
        }
    }
}
