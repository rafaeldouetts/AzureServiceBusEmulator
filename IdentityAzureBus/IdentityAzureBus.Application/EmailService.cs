using IdentityAzureBus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace IdentityAzureBus.Application
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey; // Sua chave de API Brevo
        private readonly string _apiUrl; // URL da API do Brevo

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["BrevoApiKey"];
            _apiUrl = configuration["BrevoUrlBase"];
        }

        public async Task<bool> SendEmailAsync(string toEmail, string toName, string subject, string textContent, string htmlContent)
        {
            try
            {
                var client = new RestClient(_apiUrl);

                var request = new RestRequest("v3/smtp/email", Method.Post);
                request.AddHeader("accept", "application/json");
                request.AddHeader("api-key", _apiKey);
                request.AddHeader("Content-Type", "application/json");

                var body = new
                {
                    sender = new { email = "r_santos07@outlook.com", name = "Rafael Douetts" }, // De quem está enviando o e-mail
                    to = new List<object>
                    {
                        new { email = toEmail, name = toName } // Para quem está enviando o e-mail
                    },
                    replyTo = new { email = "r_santos07@outlook.com", name = "Rafael Douetts" },
                    subject = subject,
                    textContent = textContent,
                    htmlContent = htmlContent
                };

                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);

                // Verifica se a resposta foi bem-sucedida
                if (response.IsSuccessful)
                {
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
                    return result.ContainsKey("result") && result["result"].ToString() == "true";
                }

                // Se não foi bem-sucedido, exibe a mensagem de erro
                Console.WriteLine($"Erro ao enviar o e-mail: {response.Content}");
                return false;
            }
            catch (Exception ex)
            {
                // Se ocorrer algum erro inesperado
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }
    }
}