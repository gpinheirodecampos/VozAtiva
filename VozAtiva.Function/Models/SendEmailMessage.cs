using System;
using System.Text.Json.Serialization;

namespace VozAtiva.Function.Models
{
    public class SendEmailMessage
    {
        [JsonPropertyName("AlertId")]
        public Guid AlertId { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Email")]
        public string Email { get; set; }
    }
}
