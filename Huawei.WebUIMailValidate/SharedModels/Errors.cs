using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Huawei.WebUIMailValidate.SharedModels
{
    public class Errors
    {
        [JsonPropertyName("smtp")]
        public string smtp { get; set; }

        [JsonPropertyName("mailfrom")]
        public string mailfrom { get; set; }

        [JsonPropertyName("rcpttp")]
        public string rcptto { get; set; }

        [JsonPropertyName("regex")]
        public string regex { get; set; }
    }
}
