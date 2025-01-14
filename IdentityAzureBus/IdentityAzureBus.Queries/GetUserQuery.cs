using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAzureBus.Queries.Dtos;
using MediatR;

namespace IdentityAzureBus.Queries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public GetUserQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; set; }
    }
}
