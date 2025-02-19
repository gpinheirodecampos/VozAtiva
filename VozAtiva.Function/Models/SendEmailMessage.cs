using System.Text.Json.Serialization;

namespace VozAtiva.Function.Models
{
    public class SendEmailMessage
    {
        [JsonPropertyName("alert_id")]
        public string AlertId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}