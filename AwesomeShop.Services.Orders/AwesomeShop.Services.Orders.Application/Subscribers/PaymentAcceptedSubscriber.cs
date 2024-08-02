using AwesomeShop.Services.Orders.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Application.Subscribers
{
    public class PaymentAcceptedSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IConnection _connection;

        private readonly IModel _channel;

        private const string QueueName = "order-service-payment-accepted";

        private const string ExchangeName = "order-service";

        private const string RoutingKey = "payment-accepted";

        public PaymentAcceptedSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            _connection = connectionFactory.CreateConnection("order-service-payment-accepted-subscriber");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, "topic", true);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, "payment-service", RoutingKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(byteArray);

                var message = JsonConvert.DeserializeObject<PaymentAccepted>(contentString);

                Console.WriteLine($"Received payment accepted event for order {message.OrderId}");

                var result = await UpdateORder(message);

                if(result)
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> UpdateORder(PaymentAccepted message)
        {
            using var scope = _serviceProvider.CreateScope();

            var orderService = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var order = await orderService.GetByIdAsync(message.OrderId);
            order.SetAsCompleted();

            await orderService.UpdateAsync(order);

            return true;
        }
    }

    public class PaymentAccepted
    {
        public Guid OrderId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
