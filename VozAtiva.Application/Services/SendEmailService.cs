using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;

namespace VozAtiva.Application.Services;

public class SendEmailService(string connectionString, ILogger logger) : ISendEmailService
{
    public async Task EnqueueSendEmailAsync(Guid alertId, string name, string email)
    {
        logger.LogInformation("EnqueueSendEmailAsync called with AlertId: {AlertId}, Name: {Name}, Email: {Email}", alertId, name, email);

        try
        {
            string queueName = "send-email";

            var queueClientOptions = new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64 // Define codificação Base64
            };

            var queueClient = new QueueClient(connectionString, queueName, queueClientOptions);

            await queueClient.CreateIfNotExistsAsync();
            logger.LogInformation("Queue {QueueName} ensured to exist.", queueName);

            var message = new SendEmailMessage
            {
                AlertId = alertId.ToString(),
                Name = name,
                Email = email
            };

            // Serializar para JSON
            var messageJson = JsonSerializer.Serialize(message);

            // Codificar em Base64
            var messageBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(messageJson));

            await queueClient.SendMessageAsync(messageJson);

            logger.LogInformation("Message enqueued successfully: {Message}", messageJson);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while enqueuing the email message.");
            throw;
        }
    }
}
