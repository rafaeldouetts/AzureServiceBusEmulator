using MediatR;

namespace IdentityAzureBus.Events
{
    public class UserCreatedEvent : INotification
    {
        public string Username { get; }
        public string Email { get; }
        public string Telefone { get; set; }

        public UserCreatedEvent(string username, string email, string telefone)
        {
            Username = username;
            Email = email;
            Telefone = telefone;
        }

    }
}
