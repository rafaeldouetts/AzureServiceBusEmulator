using MediatR;

namespace IdentityAzureBus.Events
{
    public class UserCreatedEvent : INotification
    {
        public string Username { get; }
        public string Email { get; }

        public UserCreatedEvent(string username, string email)
        {
            Username = username;
            Email = email;
        }

    }
}
