using IdentityAzureBus.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace IdentityAzureBus.Functions
{
    public class GetUserFunction
    {
        private readonly ILogger<GetUserFunction> _logger;
        private readonly IMediator _mediator;
        public GetUserFunction(ILogger<GetUserFunction> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function("GetUserFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string UserId= "";

            // Cria a consulta
            var query = new GetUserQuery(UserId);

            // Envia a consulta ao MediatR
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return new NotFoundObjectResult($"User with username {user} not found.");
            }

            // Retorna o usuário encontrado
            return new OkObjectResult(user);
        }
    }
}
