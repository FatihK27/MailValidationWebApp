using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Huawei.RabbitMqSubscriberService.ValidationModels
{
    public class Configuration
    {
        [JsonPropertyName("validation_type_by_domain")]
        public object validation_type_by_domain { get; set; }

        [JsonPropertyName("whitelist_validation")]
        public bool? whitelist_validation { get; set; }

        [JsonPropertyName("whitelisted_emails")]
        public object whitelisted_emails { get; set; }

        [JsonPropertyName("blacklisted_emails")]
        public object blacklisted_emails { get; set; }

        [JsonPropertyName("whitelisted_domains")]
        public object whitelisted_domains { get; set; }

        [JsonPropertyName("blacklisted_domains")]
        public object blacklisted_domains { get; set; }

        [JsonPropertyName("blacklisted_mx_ip_addresses")]
        public object blacklisted_mx_ip_addresses { get; set; }

        [JsonPropertyName("dns")]
        public object dns { get; set; }

        [JsonPropertyName("not_rfc_mx_lookup_flow")]
        public bool? not_rfc_mx_lookup_flow { get; set; }

        [JsonPropertyName("smtp_fail_fast")]
        public bool? smtp_fail_fast { get; set; }

        [JsonPropertyName("smtp_safe_check")]
        public bool? smtp_safe_check { get; set; }

        [JsonPropertyName("email_pattern")]
        public string email_pattern { get; set; }

        [JsonPropertyName("smtp_error_body_pattern")]
        public string smtp_error_body_pattern { get; set; }
    }
}
