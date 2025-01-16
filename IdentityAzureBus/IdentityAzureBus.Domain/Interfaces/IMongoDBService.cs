using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAzureBus.Domain.Interfaces
{
    public interface IMongoDBService
    {
        Task SaveFailedMessageAsync(string messageId, string error, string body);
    }
}
