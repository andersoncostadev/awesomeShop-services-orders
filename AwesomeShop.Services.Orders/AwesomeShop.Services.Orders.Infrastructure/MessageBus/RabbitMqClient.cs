using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System.Text;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus
{
    public class RabbitMqClient : IMessageBusClient
    {
        private readonly IConnection _connection;

        public RabbitMqClient(ProducerConnection producerConnection)
        {
            _connection = producerConnection.Connection;
        }
        public void Publish(object message, string routingKey, string exchange)
        {
            var channel = _connection.CreateModel();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string payload = JsonConvert.SerializeObject(message, settings);
            var body = Encoding.UTF8.GetBytes(payload);

            channel.ExchangeDeclare(exchange: exchange,type: "topic",durable: true,autoDelete: false,arguments: null);

            channel.BasicPublish(exchange: exchange,routingKey: routingKey,basicProperties: null,body: body);
        }
    }
}
