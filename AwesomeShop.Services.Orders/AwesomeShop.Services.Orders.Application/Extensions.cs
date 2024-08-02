using AwesomeShop.Services.Orders.Application.Commands;
using AwesomeShop.Services.Orders.Application.Subscribers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Orders.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddOrder));

            return services;
        }

        public static IServiceCollection AddSubscribers(this IServiceCollection services)
        {
            services.AddHostedService<PaymentAcceptedSubscriber>();

            return services;
        }
    }
}
