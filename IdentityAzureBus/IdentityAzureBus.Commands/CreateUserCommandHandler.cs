using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAzureBus.Common;
using IdentityAzureBus.Events;
using MediatR;

namespace IdentityAzureBus.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Lógica para criar o usuário (simulação)
            var user = new User
            {
                Name = request.Name,
                Email = request.Email
            };
            
            // Criação do evento
            var userCreatedEvent = new UserCreatedEvent(request.Name, request.Email);

            // Disparar o evento através do MediatR
            await _mediator.Publish(userCreatedEvent, cancellationToken);


            return new Result
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };
        }
    }

}
