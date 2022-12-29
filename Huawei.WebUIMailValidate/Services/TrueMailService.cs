using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Net.Mail;
using System;
using Huawei.WebUIMailValidate.SharedModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Huawei.WebUIMailValidate.Services
{
    public class TrueMailService:ControllerBase
    {
        public string _mailAddress { get; set; }
        private readonly ILogger<TrueMailService> _logger;
        private readonly IConfiguration _configuration;
        public TrueMailService(string mailAddress)
        {
            _mailAddress= mailAddress;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public  ValidationResult GetValidationResult()
        {
            Root vresult = new Root();
            ValidationResult response= new ValidationResult();
            string errorDescription = "";
            try
            {
                var clientHandler = new HttpClientHandler();
                using (var http = new HttpClient(clientHandler))
                {
                    //var endpoint = "http://190.92.217.218:9292?email="+mailAddress;
                    var endpoint = _configuration.GetValue<string>("MailValidationService:ServiceUrl") + "?email=" + _mailAddress;
                    http.DefaultRequestHeaders.Add("Authorization", _configuration.GetValue<string>("MailValidationService:ValidationToken"));
                    var result = http.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;
                    vresult = JsonConvert.DeserializeObject<Root>(json);
                }

                if (vresult.errors != null)
                {
                    if (vresult.errors.smtp != null)
                    {
                        errorDescription = vresult.smtp_debug[0].connection == false ? vresult.smtp_debug[0].errors.mailfrom : vresult.smtp_debug[0].errors.rcptto;
                    }
                    if (vresult.errors.regex != null)
                    {
                        errorDescription = vresult.errors.regex;
                    }
                }
                var success = vresult.success;
                ValidationResult v = new();
                v.mailAddress = _mailAddress;
                v.result = (bool)success ? "Başarılı" : "Başarısız";
                v.status = success.Value;
                v.description = errorDescription;
                response = v;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return response;
        }
    }
}

