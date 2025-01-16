using MongoDB.Bson;

namespace IdentityAzureBus.Common
{
    public class FailedMessage
    {
        public ObjectId Id { get; set; }  // MongoDB Id
        public string MessageId { get; set; }  // ID da mensagem
        public string Error { get; set; }  // Descrição do erro
        public string Body { get; set; } // Corpo da mensagem
        public DateTime Timestamp { get; set; }  // Hora do erro
    }
}
