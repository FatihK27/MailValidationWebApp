using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Huawei.WebUIMailValidate.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish<T>(T parameter) where T: class
        {
            var channel = _rabbitMQClientService.Connect();
            var bodyString = JsonSerializer.Serialize<T>(parameter);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingKey, basicProperties: properties, body: bodyByte);
        }
    }
}
