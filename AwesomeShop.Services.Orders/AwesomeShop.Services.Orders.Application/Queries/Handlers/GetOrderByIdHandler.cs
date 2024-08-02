using AwesomeShop.Services.Orders.Application.Dtos.ViewModels;
using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Infrastructure.CacheStorage;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeShop.Services.Orders.Application.Queries.Handlers
{
    public class GetOrderbyIdHandler : IRequestHandler<GetOrderById, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICacheService _cacheService;

        public GetOrderbyIdHandler(IOrderRepository orderRepository, ICacheService cacheService)
        {
            _orderRepository = orderRepository;
            _cacheService = cacheService;
        }

        public async Task<OrderViewModel> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var cacheKey = request.Id.ToString();
            var orderViewModel = await _cacheService.GetAsync<OrderViewModel>(cacheKey);

            if (orderViewModel == null)
            {
                var oder = await _orderRepository.GetByIdAsync(request.Id);

                orderViewModel = OrderViewModel.FromOrder(oder);

                await _cacheService.SetAsync(cacheKey, orderViewModel);
            }

            return orderViewModel;
        }
    }
}
