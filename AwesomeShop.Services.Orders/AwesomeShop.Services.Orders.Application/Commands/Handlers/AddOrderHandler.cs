using AwesomeShop.Services.Orders.Application.Dtos.IntegrationDtos;
using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Infrastructure;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus;
using AwesomeShop.Services.Orders.Infrastructure.ServiceDiscovery;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Application.Commands.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IServiceDiscoveryService _serviceDiscovery;

        public AddOrderHandler(IOrderRepository orderRepository,IMessageBusClient messageBusClient, IServiceDiscoveryService serviceDiscovery)
        {
            _orderRepository = orderRepository;
            _messageBusClient = messageBusClient;
            _serviceDiscovery = serviceDiscovery;
        }

        public async Task<Guid> Handle(AddOrder request, CancellationToken cancellationToken)
        {
            var order = request.ToOrder();

            var customerServiceUri = await _serviceDiscovery.GetServiceDiscoveryUri("Customer-Services", $"api/customers/{order.Customer.Id}");

            if (customerServiceUri == null)
            {
                throw new Exception("Customer service not found");
            }

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(customerServiceUri);
            var stringResult = await response.Content.ReadAsStringAsync();

            var customerDto = JsonConvert.DeserializeObject<GetCustomerByIdDto>(stringResult);

            Console.WriteLine(customerDto.FullName);


            await _orderRepository.AddAsync(order);

            foreach (var @event in order.Events)
            {
                var routingKey = @event.GetType().Name.ToDashCase();

                _messageBusClient.Publish(@event, routingKey, "oder-service");
            }

            return order.Id;
        }
    }
}
