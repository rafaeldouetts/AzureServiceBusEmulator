using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAzureBus.Common;
using IdentityAzureBus.Domain.Interfaces;
using MongoDB.Driver;

namespace IdentityAzureBus.Application
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoCollection<FailedMessage> _failedMessageCollection;

        public MongoDBService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("ErrosDatabase");
            _failedMessageCollection = database.GetCollection<FailedMessage>("failedMessages");
        }

        public async Task SaveFailedMessageAsync(string messageId, string error, string body)
        {
            try
            {
                var failedMessage = new FailedMessage
                {
                    MessageId = messageId,
                    Error = error,
                    Body = body,
                    Timestamp = DateTime.UtcNow
                };

                // Salvando a mensagem de falha no MongoDB
                await _failedMessageCollection.InsertOneAsync(failedMessage);
              
                Console.WriteLine("Mensagem salva no MongoDB devido a falha no processamento.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar mensagem no MongoDB: {ex.Message}");
            }
        }
    }
}
