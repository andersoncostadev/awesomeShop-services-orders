using AwesomeShop.Services.Orders.Application.Dtos.ViewModels;
using MediatR;
using System;

namespace AwesomeShop.Services.Orders.Application.Queries
{
    public class GetOrderById : IRequest<OrderViewModel>
    {
        public GetOrderById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
