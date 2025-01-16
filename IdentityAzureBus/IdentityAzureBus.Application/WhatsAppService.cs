using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using IdentityAzureBus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace IdentityAzureBus.Application
{
    public class WhatsAppService : IWhatsappService
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonSerializerOptions;
        private readonly string accessToken;
        private readonly string version;
        private readonly string numeroRemetente;
        private readonly IConfiguration _configuration;
        public WhatsAppService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            accessToken = configuration["TokenWhatsapp"];
            version = configuration["VersaoAPIWhatsapp"];
            numeroRemetente = configuration["TelefoneRemetenteWhatsapp"];
        }

        public async Task<object> AdicionarNumero(string numero)
        {
            var apiUrl = $"{version}/{numeroRemetente}/register";

            var body = new AdicionarNumeroViewModel(numero);

            var content = new StringContent(JToken.FromObject(body).ToString(), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.PostAsync(apiUrl, content);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);
        }

        public async Task<object> EnviarMensagem(string numero, string template)
        {
            var urlBase = _configuration["urlBaseWhatsapp"];

            var apiUrl = $"{urlBase}/{version}/{numeroRemetente}/messages";

            var body = new MessageRequest(numero, template);

            var content = new StringContent(JToken.FromObject(body).ToString(), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.PostAsync(apiUrl, content);

            var teste = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);
        }
        public async Task<Object> ObterNumerosCadastrados(string idProjeto)
        {
            var apiUrl = $"{version}/{idProjeto}/phone_numbers";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);

        }

        public async Task<Object> ObterNumeroCadastrado(string idNumero)
        {
            var apiUrl = $"{version}/{idNumero}/phone_numbers";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);
        }

        public Task SendSmsAsync(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }
    }


    public class MessageRequest
    {
        public MessageRequest(string to, string template, List<Parameters> parameters = null)
        {
            this.to = to;
            this.template = new TemplateModel(template);
            this.parameters = parameters;
        }

        public string messaging_product { get => "whatsapp"; }
        public string to { get; set; }
        public string type { get => "template"; }
        public TemplateModel template { get; set; }
        public List<Parameters> parameters { get; set; }
    }

    public class TemplateModel
    {
        public TemplateModel(string name)
        {
            this.name = name;
            language = new IdiomaModel();
        }

        public string name { get; set; }
        public IdiomaModel language { get; set; }
    }

    public class IdiomaModel
    {
        public string code { get => "en_US"; }
    }

    public enum TipoMensagem
    {
        template = 0
    }

    public class Parameters
    {
        public Parameters(string value)
        {
            Text = value;
        }

        public string Type { get => "text"; }
        public string Text { get; set; }
    }

    public class AdicionarNumeroViewModel
    {
        public AdicionarNumeroViewModel(string pin)
        {
            this.pin = pin;
        }

        public string messaging_product { get => "whatsapp"; }
        public string pin { get; set; }
    }
}
