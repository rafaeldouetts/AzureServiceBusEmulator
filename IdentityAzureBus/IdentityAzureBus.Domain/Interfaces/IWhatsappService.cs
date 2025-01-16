using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAzureBus.Domain.Interfaces
{
    public interface IWhatsappService
    {
        Task<Object> AdicionarNumero(string numero);
        Task<Object> EnviarMensagem(string numero, string template);
    }
}
