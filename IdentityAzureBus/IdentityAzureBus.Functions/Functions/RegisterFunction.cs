using System.Net;
using IdentityAzureBus.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IdentityAzureBus.Functions.Functions
{
    public class RegisterFunction
    {
        private readonly ILogger<RegisterFunction> _logger;
        private readonly IMediator _mediator;

        public RegisterFunction(ILogger<RegisterFunction> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function("Register")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                // Lê o corpo da requisição (JSON) e desserializa em um objeto de comando
                string content = await new StreamReader(req.Body).ReadToEndAsync();

                var command = JsonConvert.DeserializeObject<CreateUserCommand>(content);

                // Envia o comando para o Mediator
                var result = await _mediator.Send(command);

                // Retorna a resposta
                return result.IsSuccess
                    ? new StatusCodeResult(StatusCodes.Status201Created)
                    : new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
