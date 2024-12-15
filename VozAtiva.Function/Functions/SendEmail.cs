using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using VozAtiva.Function.Application.Email;
using VozAtiva.Function.Models;

namespace VozAtiva.Function.Functions
{
    public class SendEmail
    {
        private readonly ILogger _logger;
        private readonly IEmailServiceClient _emailServiceClient;

        public SendEmail(ILoggerFactory loggerFactory, IEmailServiceClient emailServiceClient)
        {
            _logger = loggerFactory.CreateLogger<SendEmail>();
            _emailServiceClient = emailServiceClient;
        }

        [Function("SendEmailFunction")]
        public async Task Run(
        [QueueTrigger("send-email", Connection = "AzureWebJobsStorage")] string queueMessage)
        {
            _logger.LogInformation($"Processing message: {queueMessage}");

            try
            {
                var alert = JsonSerializer.Deserialize<SendEmailMessage>(queueMessage);

                await _emailServiceClient.SendEmailAsync(alert);

                _logger.LogInformation("Email enviado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar a mensagem: {ex.Message}");
            }
        }
    }
}
