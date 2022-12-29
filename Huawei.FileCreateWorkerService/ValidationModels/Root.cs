using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Huawei.RabbitMqSubscriberService.ValidationModels
{
    public class Root
    {
        [JsonPropertyName("date")]
        public string date { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("validation_type")]
        public string validation_type { get; set; }

        [JsonPropertyName("success")]
        public bool? success { get; set; }

        [JsonPropertyName("errors")]
        public Errors errors { get; set; }

        [JsonPropertyName("smtp_debug")]
        public List<SmtpDebug> smtp_debug { get; set; }

        [JsonPropertyName("configuration")]
        public Configuration configuration { get; set; }
    }
}
