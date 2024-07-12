using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Infrastructure;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Application.Commands.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBusClient _messageBusClient;

        public AddOrderHandler(IOrderRepository orderRepository,IMessageBusClient messageBusClient)
        {
            _orderRepository = orderRepository;
            _messageBusClient = messageBusClient;
        }

        public async Task<Guid> Handle(AddOrder request, CancellationToken cancellationToken)
        {
            var order = request.ToOrder();

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
