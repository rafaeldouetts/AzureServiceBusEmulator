using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAzureBus.Common;
using MediatR;

namespace IdentityAzureBus.Commands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
